import time
import sys
import numpy as np
from concurrent.futures import ThreadPoolExecutor
import os

from ultralytics import YOLO
import cv2


""" 
    The program handles both the calibration for the camera to achieve the Homography transformation from
    2D Pixel Coordinate to 2D World Coordinate.
        .The camera must be positioned so that the moving direction of conveyor belt is from lower side to
        upper side of the FOV.
        .Each calibration needs just one checkerboard picture.
        .There are 12 columns and 9 rows within the checkerboard.
        .The y_com of detected bottle is relative to the lower side of the FOV, and in Pulse unit (number of Pulses).
"""


# C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\001_Modbus_003_ModernUI\\image\\captured_image0.png
# C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\checkerboard\\images_002.png
# C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\checkerboard\\CheckerBoard.jpg
# C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\output_dir\\images\\0f38a657__data_rac%5Cimage_113.png
# C:\Users\QUOCHUY\Desktop\0_2_IUDLab_BackUp\4_Modbus\001_Modbus_003_ModernUI\image/captured_image0.png

weight_path = 'C:\\Users\\QUOCHUY\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\last.engine'
output_directory = "C:/Users/QUOCHUY/Desktop/0_2_IUDLab_BackUp/4_Modbus/001_ModbusTest_002/processed_image"
confidence_score_threshold = 0.8
count = 0

CHECKBOARD_SIZE = (11, 8)                               # Checkerboard number of columns and rows
SQUARE_BORDER_SIZE = 15                                 # Square's Borders size in mm
MAX_Y_FOV = 1200
H_matrix = np.zeros([3, 3], dtype=np.float32)  
coor_x_origin, coor_y_origin = 0, 0
base_y_world = 0
is_calibration = True

camera_num = 1
MM_2_PULSE = 600 / 149


def draw_point(image, x, y, color=(0, 0, 255), radius=5, thickness=-1):
    """
    Draw a point on the image using cv2.circle.
    
    Args:
        image (numpy.ndarray): Input image.
        x (int): X-coordinate of the point.
        y (int): Y-coordinate of the point.
        color (tuple): BGR color of the point (default = red).
        radius (int): Radius of the point (default = 5).
        thickness (int): Thickness of the circle outline.
                        Use -1 to fill the circle.
    
    Returns:
        numpy.ndarray: Image with the point drawn.
    """
    cv2.circle(image, (int(x), int(y)), radius, color, thickness)
    cv2.imwrite("C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\checkerboard\\Hello.jpg",
                img= image)
    return


def homography_matrix(image_path,
                      checkerboard_size,
                      square_border_size):
    """ 
    @brief: This function computes the Homography Matrix (Projection Matrix)
                which maps the 2D World Coordinate (in m) to 2D Image Coordinate (in pixels)
    @args:  
            image_path          - string: path to existing image which includes a CheckerBoard
            checkerboard_size   - tuple:  inner corners (rows, cols)
            square_border_size  - float:  size of border of sub-square in reality
    @return:
            H                   - np.matrix:  Homography Matrix 
    """    
    # global coor_x_origin, coor_y_origin
    global base_y_world
    
    ## Define default message
    message = "Successfully Calibrated"
    H = -1
    
    rows, cols = checkerboard_size
    
    ## Handle the Image reading
    try:
        img = cv2.imread(image_path)
    except Exception as e:
        message = "Could not open file"
        return message, H
    
    gray_img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    
    ## Detect the CheckerBoard Corners
    try:
        retv, corners = cv2.findChessboardCorners(gray_img, (cols, rows), flags=cv2.CALIB_CB_ADAPTIVE_THRESH)
    except Exception as e:
        message = "Could not detect ChessBoard"
        return message, H
    
    ## Refine CheckerBoard locations into sub-pixel
    try:
        corners = cv2.cornerSubPix(
            gray_img, corners, (11, 11), (-1, -1),
            criteria=(cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 30, 0.001)
        )
    except Exception as e:
        return "Could not detect ChessBoard", H
    
    ## Draw the CheckerBoard onto the captured Image
    processed_img = cv2.drawChessboardCorners(cv2.cvtColor(img, cv2.COLOR_BGR2RGB), (cols,rows), corners, retv)
    
    obj_corners = np.zeros(((rows * cols), 2), dtype=np.float32)
    
    ## The corresponding reference points in World Frame
    for i in range(cols):
        for j in range(rows):
            # obj_corners[i * cols + j] = [j * square_border_size, i * square_border_size]
            obj_corners[i * cols + j] = [i * square_border_size, j * square_border_size]
            
    ## The Projection Matrix from World Coordinate to Pixel Coordinate
    H, _ = cv2.findHomography(obj_corners, corners.reshape(-1, 2), method=cv2.RANSAC)

    ## Draw CheckerBoard's axes 
    origin = project_world_2_img(H, (0, 0))
    
    _, base_y_world = project_img_2_world(H, (1000, 1200))
    
    x_axis = project_world_2_img(H, (square_border_size*3, 0))   # extend 3 squares along X
    y_axis = project_world_2_img(H, (0, square_border_size*3))   # extend 3 squares along Y
        
    # Draw axes
    cv2.arrowedLine(processed_img, origin, x_axis, (0,0,255), 5, tipLength=0.1)  # X-axis in red
    cv2.arrowedLine(processed_img, origin, y_axis, (0,255,0), 5, tipLength=0.1)  # Y-axis in green
    cv2.putText(processed_img, "X", x_axis, cv2.FONT_HERSHEY_SIMPLEX, 1, (0,0,255), 2)
    cv2.putText(processed_img, "Y", y_axis, cv2.FONT_HERSHEY_SIMPLEX, 1, (0,255,0), 2)
    
    root, _ = os.path.splitext(image_path)
    cv2.imwrite(root + "_result.jpg", processed_img)

    return message, H


def project_world_2_img(H, point):
    """Project a world point (x,y) into image using homography."""
    world = np.array([point[0], point[1], 1.0])
    img_pt = H @ world
    img_pt = img_pt / img_pt[2]
    return (int(img_pt[0]), int(img_pt[1]))


def project_img_2_world(H, point):
    """Project a world point (x,y) into image using homography."""
    img_pt = np.array([point[0], point[1], 1.0])
    world_pt = np.linalg.inv(H) @ img_pt
    world_pt = world_pt / world_pt[2]
    return (float(world_pt[0]), float(world_pt[1]))
    

def process_image(line, local_count):
    """
    @brief: This function parses the user input argument and 
                store the processed image
            The output message is organized as follow:
            Line_Index==||Bottle_ID1-Y_COM1||Bottle_ID2-Y_COM2||Bottle_ID3-Y_COM3
    """
    global model
    line = line.replace("\\", "/").strip("\n")

    img = cv2.imread(line)
        
    result = model(img, conf=confidence_score_threshold, verbose=False, device=0, half=True,
                  classes=[2,4,5,6])
    output_file = os.path.join(output_directory, f"{local_count}.jpg")
    
    result[0].save(output_file)
    boxes = result[0].obb
    
    id = -1
    if not boxes:
        return output_file, f"{0}==||"
    
    queue = set()
    
    for box in boxes:
        if box.cls in (2, 4, 5, 6):
            (x_com, y_com, _, _, _)= (box.xywhr.squeeze(0)).float().cpu().numpy()
            
            class_id = int(box.cls)
            if class_id == 2:
                id = 0
            elif class_id == 4:
                id = 1
            elif class_id == 5:
                id = 2
            else:
                id = 3
            
            (_, y_world) = project_img_2_world(H_matrix, (x_com, y_com))
            queue.add((id, int((y_world - base_y_world) * MM_2_PULSE)))                                                                 # Coordinate relative to lower side of the FOV
            
    part = (f"||{_id}-{_y_com}" for (_id, _y_com) in sorted(queue))
    message =f"{0}==" + "".join(part)

    return output_file, message


def model__init__():
    """ 
    @brief: This function initializes the model once.
            The assigned task is Oriented Bounding Box
    """
    global model
    model = YOLO(weight_path, task='obb')
    
    
def camera_calib(line):
    """ 
    @brief: This function receives the captured image for calibration, which includes
                a CheckerBoard, and returns the Homography Matrix 
    """
    global H_matrix
    global is_calibration
    
    src_img_path = line.replace("\\", "/").strip("\n")
    retv_message, H_matrix = homography_matrix(src_img_path,
                                                CHECKBOARD_SIZE,
                                                SQUARE_BORDER_SIZE)
    if retv_message == "Successfully Calibrated":
        print("Successfully Calibrated", flush=True)
        print(src_img_path.strip(".jpg") + "_result.jpg", flush=True)                                    
        is_calibration = False
    
    else:
        print("Calibration Failed: " + retv_message, flush=True)
        print(src_img_path, flush=True)
        is_calibration = True
        
def model_inference(line):
    """ 
    @brief: This function receives the captured image for inference, which includes
                the concerning objects. The return values are organized as follows:
            - Number of detected objects
            - Processed Image file path
            - Each object's ID and values, this is compressed into pair value <ID, Val>
    """
    global count
    try:
            line = line.replace("\\", "/").strip("\n")
            print(f"{camera_num * 2}", flush=True)
            _output_file, _message = process_image(line, count)
            print(_output_file, flush=True)
            print(_message, flush=True)
                
            count += 1
            if (count >= 10):
                count = 0
            
    except KeyboardInterrupt:
        a = 0


def main_func():
    model__init__()
    for line in sys.stdin:
        if(is_calibration):
            camera_calib(line)
        else:
            model_inference(line)


if __name__ == "__main__":    
    main_func()
        
        
        
        
        
        
        
        
