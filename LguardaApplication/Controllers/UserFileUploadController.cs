using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LgurdaApp.Model.ControllerModels;
using LguardaApplication.Utility;
using System.Configuration;
using System.IO;
using LguardaApp.RBAC.Action_Filters;
using Futronic.SDKHelper;
using System.Drawing;
using System.Text;


namespace LguardaApplication.Controllers
{
    //[RBAC]
    public class UserFileUploadController : Controller
    {
        #region CLASS LVEL VARIABLE
        LG_USER_FILE_UPLOAD_MAP OBJ_LG_USER_FILE_UPLOAD_MAP = new LG_USER_FILE_UPLOAD_MAP();
        string BiometricPath = ConfigurationManager.AppSettings["BiometricPath"];
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            string result = string.Empty;

            try
            {
                //string url = Utility.AppSetting.getLgardaServer() + "/Get_All_User_Id_For_DD" + "?format=json";
                //OBJ_LG_USER_FILE_UPLOAD_MAP.LIST_ALL_USER_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

                var url1 = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_UserUploadFileType_ForDD" + "?format=json";
                var file_type_list = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                ViewData["FileTypeList"] = file_type_list;
                ViewData["Success"] = TempData["Success"];
                return View(OBJ_LG_USER_FILE_UPLOAD_MAP);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Service is unable to process the request.";
                OBJ_LG_USER_FILE_UPLOAD_MAP.ERROR = ex.Message;
                return View(OBJ_LG_USER_FILE_UPLOAD_MAP);
            }
        }
        /*
        [HttpPost]
        public ActionResult Index(LG_USER_FILE_UPLOAD_MAP pLG_USER_FILE_UPLOAD_MAP, string command)
        {
            string result = string.Empty;

            try
            {
                OBJ_LG_USER_FILE_UPLOAD_MAP.USER_SESSION_ID = Session["currentUserID"].ToString();
                //string url = Utility.AppSetting.getLgardaServer() + "/Get_All_User_Id_For_DD" + "?format=json";
                //OBJ_LG_USER_FILE_UPLOAD_MAP.LIST_ALL_USER_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        pLG_USER_FILE_UPLOAD_MAP.APPLICATION_ID = "01";

                        if (pLG_USER_FILE_UPLOAD_MAP.FILE_TYPE == 6)
                        {
                            pLG_USER_FILE_UPLOAD_MAP.imageByte = ReadThumbPic();
                        }
                        else
                        {
                            //for image 
                            MemoryStream target = new MemoryStream();
                            pLG_USER_FILE_UPLOAD_MAP.File.InputStream.CopyTo(target);
                            byte[] ImageData = target.ToArray();
                            pLG_USER_FILE_UPLOAD_MAP.File = null;
                            pLG_USER_FILE_UPLOAD_MAP.imageByte = ImageData;
                        }

                        string u = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_File?format=json";
                        Uri uri = new Uri(u);

                        result = HttpWcfRequest.PostObject_WithReturningString<LG_USER_FILE_UPLOAD_MAP>(uri, pLG_USER_FILE_UPLOAD_MAP);

                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            DeleteThumbPic();
                            System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            DeleteThumbPic();
                            System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                            return View("Index");
                        }
                    }
                    else
                    {
                        var file_type_list = new List<SelectListItem>
                                {
                                    new SelectListItem{ Text="Photograph", Value = "1" },
                                    new SelectListItem{ Text="Signature", Value = "2" },
                                    new SelectListItem{ Text="Document", Value = "3" },
                                    new SelectListItem{ Text="Others", Value = "4" },
                                };

                        ViewData["FileTypeList"] = file_type_list;

                        DeleteThumbPic();
                        System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                        return View(OBJ_LG_USER_FILE_UPLOAD_MAP);
                    }
                }
                else if (command == "Show")
                {
                    return RedirectToAction("ShowThumbpic", pLG_USER_FILE_UPLOAD_MAP);
                }
                else if (command == "Put your thumb")
                {
                    var url2 = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_UserUploadFileType_ForDD" + "?format=json";
                    var file_type_list = HttpWcfRequest.GetObjectCollection<SelectListItem>(url2);
                    ViewData["FileTypeList"] = file_type_list;

                    if (string.IsNullOrEmpty(pLG_USER_FILE_UPLOAD_MAP.USER_ID))
                    {
                        ModelState.AddModelError("USER_ID", "Please provide user id.");
                        return View(pLG_USER_FILE_UPLOAD_MAP);
                    }
                    var url1 = ConfigurationManager.AppSettings["lgarda_server"].ToString() + "/Get_UserUploadFile_ByUserId/" + pLG_USER_FILE_UPLOAD_MAP.USER_ID + "?format=json";
                    LG_USER_FILE_UPLOAD_MAP Lg_User_file_Upload_Info = HttpWcfRequest.GetObject<LG_USER_FILE_UPLOAD_MAP>(url1);

                    if (Lg_User_file_Upload_Info == null)
                    {
                        ModelState.AddModelError("USER_ID", "User id doesn't exists.");
                        return View(pLG_USER_FILE_UPLOAD_MAP);
                    }

                    //System.IO.File.WriteAllBytes(Server.MapPath("~/Images/thumbpic/Default"), Lg_User_file_Upload_Info.imageByte);
                    System.IO.File.WriteAllBytes(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"), Lg_User_file_Upload_Info.imageByte);
                    VerifyFingerPrint();
                    //System.IO.File.Delete(Server.MapPath("~/Images/thumbpic/Default"));
                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                    return View(pLG_USER_FILE_UPLOAD_MAP);
                }
                else if (command == "Verify")
                {
                    string dataTxtFile = IsVerifiedThumbPrint(Server.MapPath(BiometricPath), "Verification.txt");
                    if (dataTxtFile.ToLower().Trim().Contains("true"))
                    {
                        ViewData["Success"] = "Verification successful.";
                    }
                    else
                        ViewData["Success"] = "Verification failed.";

                    string pathString = System.IO.Path.Combine(Server.MapPath(BiometricPath), "Verification.txt");
                    if (System.IO.File.Exists(pathString))  //if txt file exists then empty txt file
                    {
                        System.IO.File.WriteAllText(pathString, String.Empty);
                    }


                    var url2 = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_UserUploadFileType_ForDD" + "?format=json";
                    var file_type_list = HttpWcfRequest.GetObjectCollection<SelectListItem>(url2);
                    ViewData["FileTypeList"] = file_type_list;
                    return View(pLG_USER_FILE_UPLOAD_MAP);
                }
                else
                {
                    DeleteThumbPic();
                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                    return View(OBJ_LG_USER_FILE_UPLOAD_MAP);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't upload file. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't upload file.";
                }

                string exception = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    string innerException = ex.InnerException.ToString();
                }

                return View("Index");
            }
        }  */

        [RBAC]
        [HttpPost]
        public ActionResult Index(LG_USER_FILE_UPLOAD_MAP pLG_USER_FILE_UPLOAD_MAP, string command)
        {
            string result = string.Empty;
            try
            {
                OBJ_LG_USER_FILE_UPLOAD_MAP.USER_SESSION_ID = Session["currentUserID"].ToString();
                var url2 = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_UserUploadFileType_ForDD" + "?format=json";
                var file_type_list = HttpWcfRequest.GetObjectCollection<SelectListItem>(url2);
                ViewData["FileTypeList"] = file_type_list;

                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        pLG_USER_FILE_UPLOAD_MAP.APPLICATION_ID = "01";

                        if (pLG_USER_FILE_UPLOAD_MAP.FILE_TYPE == 6)
                        {
                            pLG_USER_FILE_UPLOAD_MAP.imageByte = ReadThumbPic();
                        }
                        else
                        {
                            //for image 
                            MemoryStream target = new MemoryStream();
                            pLG_USER_FILE_UPLOAD_MAP.File.InputStream.CopyTo(target);
                            byte[] ImageData = target.ToArray();
                            pLG_USER_FILE_UPLOAD_MAP.File = null;
                            pLG_USER_FILE_UPLOAD_MAP.imageByte = ImageData;
                        }

                        string u = ConfigurationManager.AppSettings["lgarda_server"].ToString() + "/Add_File?format=json";
                        Uri uri = new Uri(u);

                        result = HttpWcfRequest.PostObject_WithReturningString<LG_USER_FILE_UPLOAD_MAP>(uri, pLG_USER_FILE_UPLOAD_MAP);

                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("ACC_NO", "Customer Already Exists.");
                            return View(pLG_USER_FILE_UPLOAD_MAP);
                        }

                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            DeleteThumbPic();
                            System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";
                            DeleteThumbPic();
                            System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                            return View("Index");
                        }
                    }
                    else
                    {
                        DeleteThumbPic();
                        System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                        return View(OBJ_LG_USER_FILE_UPLOAD_MAP);
                    }
                }
                else if (command == "Show")
                {
                    return RedirectToAction("ShowThumbpic", pLG_USER_FILE_UPLOAD_MAP);
                }
                else if (command == "Put your thumb")
                {
                    if (string.IsNullOrEmpty(pLG_USER_FILE_UPLOAD_MAP.USER_ID))
                    {
                        ModelState.AddModelError("USER_ID", "Please provide user id.");
                        return View(pLG_USER_FILE_UPLOAD_MAP);
                    }
                    var url1 = ConfigurationManager.AppSettings["lgarda_server"].ToString() + "/Get_UserUploadFile_ByUserId/" + pLG_USER_FILE_UPLOAD_MAP.USER_ID + "?format=json";
                    LG_USER_FILE_UPLOAD_MAP Lg_User_file_Upload_Info = HttpWcfRequest.GetObject<LG_USER_FILE_UPLOAD_MAP>(url1);

                    if (Lg_User_file_Upload_Info == null)
                    {
                        ModelState.AddModelError("USER_ID", "User id doesn't exists.");
                        return View(pLG_USER_FILE_UPLOAD_MAP);
                    }

                    //System.IO.File.WriteAllBytes(Server.MapPath("~/Images/thumbpic/Default"), Lg_User_file_Upload_Info.imageByte);
                    System.IO.File.WriteAllBytes(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"), Lg_User_file_Upload_Info.imageByte);
                    VerifyFingerPrint();
                    //System.IO.File.Delete(Server.MapPath("~/Images/thumbpic/Default"));
                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                    return View(pLG_USER_FILE_UPLOAD_MAP);
                }
                else if (command == "Verify")
                {
                    string dataTxtFile = IsVerifiedThumbPrint(Server.MapPath(BiometricPath), "Verification.txt");
                    if (dataTxtFile.ToLower().Trim().Contains("true"))
                    {
                        ViewData["Success"] = "Verification successful.";
                    }
                    else
                        ViewData["Success"] = "Verification failed.";

                    string pathString = System.IO.Path.Combine(Server.MapPath(BiometricPath), "Verification.txt");
                    if (System.IO.File.Exists(pathString))  //if txt file exists then empty txt file
                    {
                        System.IO.File.WriteAllText(pathString, String.Empty);
                    }
                    return View(pLG_USER_FILE_UPLOAD_MAP);
                }
                else
                {
                    DeleteThumbPic();
                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath(BiometricPath), "Default"));
                    return View(OBJ_LG_USER_FILE_UPLOAD_MAP);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't upload file. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't upload file.";
                }

                string exception = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    string innerException = ex.InnerException.ToString();
                }

                return View("Index");
            }
        }



        #region Variables
        private bool IsExit;
        private Object operationOBJ;
        private FutronicSdkBase futronicSdkBaseOperationOBJ;

        delegate void SetTextCallback(string text);
        delegate void SetIdentificationLimitCallback(int limit);
        delegate void EnableControlsCallback(bool bEnable);
        delegate void SetImageCallback(Bitmap hBitmap);

        private String databaseDir;
        private bool IsInitializationSuccess;
        int capturedFingerPrintNum = 1;
        #endregion

        #region Custom Method
        [HttpGet]
        public JsonResult Get_UserInfoByUserId(string pUSER_ID)
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserInfoByUserId/" + pUSER_ID + "?format=json";
                OBJ_LG_USER_FILE_UPLOAD_MAP = HttpWcfRequest.GetObject<LG_USER_FILE_UPLOAD_MAP>(url);
                if (OBJ_LG_USER_FILE_UPLOAD_MAP != null)
                {
                    return Json(OBJ_LG_USER_FILE_UPLOAD_MAP, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region Biometric(Enroll & Save)
        [RBAC]
        public ActionResult Enroll()
        {
            #region Initialize Form & default values

            // Create FutronicEnrollment object for retrieve default values only
            FutronicEnrollment dummy = new FutronicEnrollment();
            //cbFARNLevel.SelectedIndex = (int)dummy.FARnLevel;
            //cbMaxFrames.SelectedItem = dummy.MaxModels.ToString();
            //chDetectFakeFinger.Checked = dummy.FakeDetection;
            //cbMIOTOff.Checked = dummy.MIOTControlOff;
            //chFastMode.Checked = dummy.FastMode;
            //SetIdentificationLimit(dummy.IdentificationsLeft);

            //buttonStop.Enabled = false;
            IsExit = false;

            //int selectedIndex = 0, itemIndex;
            //foreach (ComboBoxItem item in rgVersionItems)
            //{
            //    itemIndex = m_cmbVersion.Items.Add(item);
            //    if ((VersionCompatible)item.Tag == dummy.Version)
            //    {
            //        selectedIndex = itemIndex;
            //    }
            //}
            //m_cmbVersion.SelectedIndex = selectedIndex;

            try
            {
                databaseDir = GetDatabaseDir();
            }
            catch (UnauthorizedAccessException)
            {

            }
            catch (IOException)
            {

            }
            #endregion

            DbRecord User = new DbRecord();

            User.UserName = "Default";

            operationOBJ = User;

            if (futronicSdkBaseOperationOBJ != null)
            {
                futronicSdkBaseOperationOBJ.Dispose();
                futronicSdkBaseOperationOBJ = null;
            }

            futronicSdkBaseOperationOBJ = new FutronicEnrollment();

            // Set control properties
            futronicSdkBaseOperationOBJ.FakeDetection = false; //chDetectFakeFinger.Checked;
            futronicSdkBaseOperationOBJ.FFDControl = true;
            futronicSdkBaseOperationOBJ.FARN = 166; //Convert.ToInt32(tbFARN.Text.Trim());
            ((FutronicEnrollment)futronicSdkBaseOperationOBJ).MIOTControlOff = false;
            ((FutronicEnrollment)futronicSdkBaseOperationOBJ).MaxModels = 1;

            //EnableControls(false);

            // register events
            futronicSdkBaseOperationOBJ.OnPutOn += new OnPutOnHandler(this.OnPutOn);
            futronicSdkBaseOperationOBJ.OnTakeOff += new OnTakeOffHandler(this.OnTakeOff);
            futronicSdkBaseOperationOBJ.UpdateScreenImage += new UpdateScreenImageHandler(this.UpdateScreenImage);
            futronicSdkBaseOperationOBJ.OnFakeSource += new OnFakeSourceHandler(this.OnFakeSource);
            ((FutronicEnrollment)futronicSdkBaseOperationOBJ).OnEnrollmentComplete += new OnEnrollmentCompleteHandler(this.OnEnrollmentComplete);

            // start enrollment process
            ((FutronicEnrollment)futronicSdkBaseOperationOBJ).Enrollment();

            return null;
        }
        [RBAC]
        private void OnPutOn(FTR_PROGRESS Progress)
        {
            this.SetStatusText("Place thumb/finger on device.");
        }
        [RBAC]
        private void SetStatusText(String text)
        {
        }
        [RBAC]
        private void OnTakeOff(FTR_PROGRESS Progress)
        {
            this.SetStatusText("Remove thumb/finger from device");
        }
        [RBAC]
        private void UpdateScreenImage(Bitmap hBitmap)
        {
            // Do not change the state control during application closing.
            if (IsExit)
                return;
            if (capturedFingerPrintNum == 1)
            {
                //hBitmap.Save(Path.Combine(Server.MapPath("~/Images/thumbpic/thumbpic.Bmp")), System.Drawing.Imaging.ImageFormat.Bmp);
                hBitmap.Save(System.IO.Path.Combine(Server.MapPath(BiometricPath), "thumbpic.Bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
            }

            else if (capturedFingerPrintNum == 2)
            {
            }

            else if (capturedFingerPrintNum == 3)
            {
            }
        }
        [RBAC]
        private bool OnFakeSource(FTR_PROGRESS Progress)
        {
            return false;
        }
        [RBAC]
        private void OnEnrollmentComplete(bool bSuccess, int nRetCode)
        {
            StringBuilder szMessage = new StringBuilder();
            if (bSuccess)
            {
                // set status string
                szMessage.Append("Enrollment of fingerprint successful");
                szMessage.Append("Quality: ");
                szMessage.Append(((FutronicEnrollment)futronicSdkBaseOperationOBJ).Quality.ToString());
                this.SetStatusText(szMessage.ToString());

                // Set template into user's information and save it
                DbRecord User = (DbRecord)operationOBJ;
                User.Template = ((FutronicEnrollment)futronicSdkBaseOperationOBJ).Template;

                String szFileName = Path.Combine(databaseDir, User.UserName);
                if (!User.Save(szFileName))
                {
                    //MessageBox.Show("Error - Cannot save user’s information to file " + szFileName, "example for Futronic SDK", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else // successful, do save to actual db and then delete the temp file
                {

                }
            }
            else
            {
                szMessage.Append("Enrollment failed.");
                szMessage.Append("Error description: ");
                szMessage.Append(FutronicSdkBase.SdkRetCode2Message(nRetCode));
                this.SetStatusText(szMessage.ToString());
            }
            operationOBJ = null;
            //EnableControls(true);
        }
        [RBAC]
        public String GetDatabaseDir()
        {

            String szDbDir;
            //szDbDir = Path.Combine(Server.MapPath("~/Images"), "thumbpic");
            szDbDir = Server.MapPath(BiometricPath);

            if (!Directory.Exists(szDbDir))
            {
                Directory.CreateDirectory(szDbDir);
            }

            return szDbDir;
        }
        [RBAC]
        public ActionResult ShowThumbpic(LG_USER_FILE_UPLOAD_MAP pLG_USER_FILE_UPLOAD_MAP)
        {
            var url1 = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_UserUploadFileType_ForDD" + "?format=json";
            var file_type_list = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
            ViewData["FileTypeList"] = file_type_list;
            return View("Index", OBJ_LG_USER_FILE_UPLOAD_MAP);
        }
        [RBAC]
        public void DeleteThumbPic()
        {
            //string fullPath = Request.MapPath("~/Images/thumbpic/" + "thumbpic.Bmp");
            string fullPath = System.IO.Path.Combine(Server.MapPath(BiometricPath), "thumbpic.Bmp");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        [RBAC]
        public byte[] ReadThumbPic()
        {
            string path = GetDatabaseDir() + "/Default";

            // if u want to read the text then use stream reader
            //using (StreamReader sr = new StreamReader())
            //{
            //    // Read the stream to a string, and write the string to the console.
            //    String line = sr.ReadToEnd();
            //}

            byte[] fileData = System.IO.File.ReadAllBytes(path);
            return fileData;

        }
        #endregion

        #region Biometric(Verification)
        [RBAC]
        private void VerifyFingerPrint()
        {
            SetStatusText("Programme is loading database, please wait ...");
            databaseDir = GetDatabaseDir();
            List<DbRecord> Users = DbRecord.ReadRecords(databaseDir);

            //SetStatusText(String.Empty);
            //if (Users.Count == 0)
            //{
            //    EnableControls(true);
            //    MessageBox.Show(this, "Error : No users enrolled in database. Run enrollment process first.",
            //    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            // select user's information for verification
            //SelectUser frmSelectUser = new SelectUser(Users, databaseDir);
            //frmSelectUser.ShowDialog(this);

            //if (frmSelectUser.SelectedUser == null)
            //{
            //    EnableControls(true);
            //    MessageBox.Show(this, "Error : No user selected", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            operationOBJ = Users[0];

            if (futronicSdkBaseOperationOBJ != null)
            {
                futronicSdkBaseOperationOBJ.Dispose();
                futronicSdkBaseOperationOBJ = null;
            }
            futronicSdkBaseOperationOBJ = new FutronicVerification(((DbRecord)operationOBJ).Template);

            // Set control properties
            // Set control properties
            futronicSdkBaseOperationOBJ.FakeDetection = false; //chDetectFakeFinger.Checked;
            futronicSdkBaseOperationOBJ.FFDControl = true;
            futronicSdkBaseOperationOBJ.FARN = 166; //Convert.ToInt32(tbFARN.Text.Trim());

            // register events
            futronicSdkBaseOperationOBJ.OnPutOn += new OnPutOnHandler(this.OnPutOn);
            futronicSdkBaseOperationOBJ.OnTakeOff += new OnTakeOffHandler(this.OnTakeOff);
            futronicSdkBaseOperationOBJ.UpdateScreenImage += new UpdateScreenImageHandler(this.UpdateVerifyImage);
            futronicSdkBaseOperationOBJ.OnFakeSource += new OnFakeSourceHandler(this.OnFakeSource);
            ((FutronicVerification)futronicSdkBaseOperationOBJ).OnVerificationComplete += new OnVerificationCompleteHandler(this.OnVerificationComplete);

            // start verification process
            ((FutronicVerification)futronicSdkBaseOperationOBJ).Verification();

            #region Empty Verification txt file if exists
            string pathString = Server.MapPath(BiometricPath);
            string fileName = "Verification.txt";
            pathString = System.IO.Path.Combine(pathString, fileName);

            if (System.IO.File.Exists(pathString))  //if txt file exists then empty txt file
            {
                System.IO.File.WriteAllText(pathString, String.Empty);
            }
            #endregion
        }
        private void UpdateVerifyImage(Bitmap hBitmap)
        {
            //if (VerifyPic.InvokeRequired)
            //{
            //    SetImageCallback d = new SetImageCallback(this.UpdateVerifyImage);
            //    this.Invoke(d, new object[] { hBitmap });
            //}
            //else
            //{
            //    VerifyPic.Image = hBitmap;
            //}

            //if (IsExit)
            //    return;
            //if (capturedFingerPrintNum == 1)
            //{
            //    hBitmap.Save(Path.Combine(Server.MapPath("~/Images/thumbpic/thumbpic_v.Bmp")), System.Drawing.Imaging.ImageFormat.Bmp);
            //}

            //else if (capturedFingerPrintNum == 2)
            //{
            //}

            //else if (capturedFingerPrintNum == 3)
            //{
            //}
        }
        [RBAC]
        private void OnVerificationComplete(bool bSuccess, int nRetCode, bool bVerificationSuccess)
        {
            StringBuilder szResult = new StringBuilder();
            if (bSuccess)
            {
                if (bVerificationSuccess)
                {
                    szResult.Append("Verification successful.");
                    szResult.Append("User Name: ");
                    szResult.Append(((DbRecord)operationOBJ).UserName);
                }
                else
                    szResult.Append("Verification failed.");

                #region Create Verification txt file & write
                string datastring = bVerificationSuccess.ToString();
                //string pathString = Server.MapPath("~/Images/thumbpic");
                string fileName = "Verification.txt";
                string pathString = System.IO.Path.Combine(Server.MapPath(BiometricPath), fileName);

                if (!System.IO.File.Exists(pathString))  //if txt file not exixts then create & write
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(datastring);
                        fs.Write(info, 0, info.Length);
                    }
                }
                else
                {
                    System.IO.File.WriteAllText(pathString, datastring);
                }
                #endregion
            }
            else
            {
                szResult.Append("Verification failed.");
                szResult.Append("Error description: ");
                szResult.Append(FutronicSdkBase.SdkRetCode2Message(nRetCode));
            }

            this.SetStatusText(szResult.ToString());
            this.SetIdentificationLimit(futronicSdkBaseOperationOBJ.IdentificationsLeft);
            operationOBJ = null;
            //EnableControls(true);
        }
        [RBAC]
        private void SetIdentificationLimit(int nLimit)
        {
            //if (this.m_lblIdentificationsLimit.InvokeRequired)
            //{
            //    SetIdentificationLimitCallback d = new SetIdentificationLimitCallback(this.SetIdentificationLimit);
            //    this.Invoke(d, new object[] { nLimit });
            //}
            //else
            //{
            //    if (nLimit == Int32.MaxValue)
            //    {
            //        m_lblIdentificationsLimit.Text = "Identification limit: No limits";
            //    }
            //    else
            //    {
            //        m_lblIdentificationsLimit.Text = String.Format("Identification limit: {0}", nLimit);
            //    }
            //}
        }
        #endregion

        [RBAC]
        public string IsVerifiedThumbPrint(string ppathString, string pfileName)
        {
            string dataTxtFile = string.Empty;
            try
            {
                string pathString = System.IO.Path.Combine(ppathString, pfileName);
                dataTxtFile = System.IO.File.ReadAllText(pathString);
                return dataTxtFile;
            }
            catch (IOException ex)
            {
                dataTxtFile = ex.Message;
                return dataTxtFile;
            }
        }
        #endregion
    }
}