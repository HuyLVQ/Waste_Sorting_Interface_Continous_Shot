using System;
using Basler.Pylon;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace _001_Modbus_003_ModernUI.Properties
{
    public class BaslerCamera
    {

        private Camera _camera;                 // Camera instance


        /// <summary>
        /// This function initializes a Camera instance and establishes the connection to the Camera
        ///     Then, the Camera settings are configured to 8-bit BRG format
        /// </summary>
        /// <returns></returns>
        public string camera_init()
        {
            try
            {
                _camera = new Camera();         // Dynamically allocating Camera instance memory
                _camera.Open();                 // Open the Camera connection
                _camera.Parameters[PLCamera.PixelFormat].SetValue(PLCamera.PixelFormat.BGR8);
                _camera.Parameters[PLCamera.ExposureTime].SetValue(5000);
                return "Camera is successfully connected";
            }
            catch (Exception ex)
            {
                return $"Camera could not be Opened\nError: {ex.Message}";
            }
        }


        /// <summary>
        /// This function closes the camera when there's no more use
        /// </summary>
        /// <returns></returns>
        public string camera_close()
        {
            try
            {
                _camera.Close();
                _camera.Dispose();
                return "OK";
            }
            catch (Exception ex)
            {
                return $"Camera could not be Closed\nError: {ex.Message}";
            }
        }


        /// <summary>
        /// This function prompts the camera to capture an image
        ///     If capturing is succeeded, the resulted image is stored under the path with name "captured_image"
        /// </summary>
        /// <param name="image_capture_path"></param>
        /// <returns></returns>
        public string camera_oneshot_capture(string image_capture_path, int count)
        {
            _camera.StreamGrabber.Start(1);
            IGrabResult grab_result;
            try
            {
                grab_result = _camera.StreamGrabber.RetrieveResult(2000, TimeoutHandling.ThrowException);
            }
            catch (Exception ex)
            { 
                return $"Image is not captured\nError: {ex.Message}";
            }
            
            _camera.StreamGrabber.Stop();
            using (grab_result)
            {
                if (grab_result.GrabSucceeded)
                {
                    try
                    {
                        ImagePersistence.Save(ImageFileFormat.Jpeg, image_capture_path + "captured_image" + count.ToString() + ".jpg", grab_result);
                        return "Image is captured successfully";
                    }
                    catch (Exception ex)
                    {
                        return $"Image is not captured\nError: {ex.Message}";
                    }
                }
                else
                { 
                    return $"Error {grab_result.ErrorCode}: {grab_result.ErrorDescription}";
                }
            }
        }

        public bool camera_is_connected()
        {
            return _camera.IsOpen;
        }

    }
}
