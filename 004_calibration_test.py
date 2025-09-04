import cv2
import numpy as np

import cv2
import numpy as np
import sys

# C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\checkerboard\\images.png
# C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\checkerboard\\images_002.png

CHECKBOARD_SIZE = (11, 8)           # Checkerboard number of columns and rows
SQUARE_BORDER_SIZE = 15          # Square's Borders size in m

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


def homography_matrix(image_path,
                      checkerboard_size,
                      square_border_size):
    """ 
    @brief: This function computes the Homography Matrix (Projection Matrix)
                which maps the 2D World Coordinate (in m) to 2D Image Coordinate (in pixels)
    @args:  
            image_path          - string: path to existing image which includes a CheckerBoard
            checkerboard_size   - tuple:  inner corners (rows, cols)
            square_border_size  - float:  size of border of sub-square
    @return:
            H                   - np.matrix:  Homography Matrix 
    """
    rows, cols = checkerboard_size
    img = cv2.imread(image_path)
    gray_img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        
    retv, corners = cv2.findChessboardCorners(gray_img, (cols, rows), flags=cv2.CALIB_CB_ADAPTIVE_THRESH)
    corners = cv2.cornerSubPix(
        gray_img, corners, (11, 11), (-1, -1),
        criteria=(cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 30, 0.001)
    )
    
    processed_img = cv2.drawChessboardCorners(cv2.cvtColor(img, cv2.COLOR_BGR2RGB), (cols,rows), corners, retv)
    cv2.imwrite(image_path.strip(".png") + "_result" + ".jpg", processed_img)    
    
    obj_corners = np.zeros(((rows * cols), 2), dtype=np.float32)
    
    for i in range(cols):
        for j in range(rows):
            obj_corners[i * cols + j] = [j * square_border_size, i * square_border_size]
            
    H, _ = cv2.findHomography(obj_corners, corners.reshape(-1, 2), method=cv2.RANSAC)
    return H


# Global variables for mouse click handling
points = []
window_name = "Select Two Points"

def mouse_callback(event, x, y, flags, param):
    """Callback function to handle mouse clicks and store selected points."""
    if event == cv2.EVENT_LBUTTONDOWN:
        points.append((x, y))
        # Draw a circle at the clicked point
        cv2.circle(param['image'], (x, y), 5, (0, 255, 0), -1)
        cv2.imshow(window_name, param['image'])
        # If two points are selected, close the window
        if len(points) == 2:
            cv2.waitKey(200)  # Show the second point briefly before closing
            cv2.destroyWindow(window_name)

def compute_world_distance(image_path, homography_matrix):
    """
    @brief: Allows user to select two points on the image and computes the Euclidean distance
            between them in the world coordinate system (in mm).
    @args:
            image_path         - string: path to the image
            homography_matrix  - np.matrix: Homography matrix from image to world coordinates
    @return:
            distance           - float: Euclidean distance in mm between the two points
    """
    # Load the image
    img = cv2.imread(image_path)
    if img is None:
        raise ValueError(f"Could not load image at {image_path}")

    # Create a copy of the image for display
    display_img = img.copy()

    # Set up the window and mouse callback
    cv2.namedWindow(window_name)
    cv2.setMouseCallback(window_name, mouse_callback, {'image': display_img})

    # Display the image and wait for user to select points
    cv2.imshow(window_name, display_img)
    cv2.waitKey(0)  # Wait until two points are selected or window is closed

    # Check if exactly two points were selected
    if len(points) != 2:
        cv2.destroyAllWindows()
        raise ValueError("Exactly two points must be selected")

    print(points[0])
    print(points[1])

    # Project the selected image points to world coordinates
    point1_world = project_img_2_world(homography_matrix, points[0])
    point2_world = project_img_2_world(homography_matrix, points[1])

    # Calculate Euclidean distance in world coordinates (in mm)
    distance = np.sqrt((point2_world[0] - point1_world[0])**2 + 
                       (point2_world[1] - point1_world[1])**2)

    # Clean up
    cv2.destroyAllWindows()

    return distance, point1_world, point2_world

if __name__ == "__main__":
    src_img_path = "C:\\Users\\QUOCHUY\\Desktop\\0_2_IUDLab_BackUp\\4_Modbus\\004_Model\\label-studio-yolov8\\checkerboard\\CheckerBoard.jpg"  # Update this path
    src_img_path = src_img_path.replace("\\", "/")
    H = homography_matrix(src_img_path, CHECKBOARD_SIZE, SQUARE_BORDER_SIZE)

    try:
        # Compute the distance between two interactively selected points
        distance, point1_world, point2_world = compute_world_distance(src_img_path, H)
        print(f"Selected points in world coordinates:")
        print(f"Point 1: ({point1_world[0]:.4f}, {point1_world[1]:.4f}) mms")
        print(f"Point 2: ({point2_world[0]:.4f}, {point2_world[1]:.4f}) mms")
        print(f"Distance between points: {distance:.4f} mms")
    except ValueError as e:
        print(f"Error: {e}")