using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DextersLabCreation1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strControllerCopy = string.Empty;
        private string strServiceCopy = string.Empty;
        private string strRepositoryCopy = string.Empty;
        private string strControllerTestCopy = string.Empty;
        private string strServiceTestCopy = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var BasePath = txtblkBasePath.Text;
                var BaseEntity = txtblkBaseEntity.Text;

                if (BasePath != "" && BaseEntity != "")
                {
                    GetController(BasePath, BaseEntity);
                    GetService(BasePath, BaseEntity);
                    GetRepository(BasePath, BaseEntity);
                    GetControllerTest(BasePath, BaseEntity);
                    GetServiceTest(BasePath, BaseEntity);
                }
                else
                {
                    MessageBox.Show("Enter both BasePath and BaseEntity");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on btnsearch_Click" + ex);
            }
        }

        /// <summary>
        /// Clone button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var BaseEntity = txtblkBaseEntity.Text;
                string FileNewpath = string.Empty;
                //Get new entity name
                var NewEntity = txtblkNewEntity.Text;

                if (NewEntity != "" && BaseEntity != "")
                {
                    //if we chnage the path of controller specifally
                    if (strControllerCopy == txtblkController.Text)
                    {
                        //Set the new file name - For Controller
                        FileNewpath = strControllerCopy.Replace(BaseEntity + "Controller.cs", NewEntity + "Controller.cs");
                        CreateClone(strControllerCopy, FileNewpath, BaseEntity, NewEntity);
                    }
                    else
                    {
                        FileNewpath = txtblkController.Text + "\\" + NewEntity + "Controller.cs";
                        CreateClone(strControllerCopy, FileNewpath, BaseEntity, NewEntity);
                    }

                    if (strServiceCopy == txtblkService.Text)
                    {
                        //Set the new file name - For Service
                        FileNewpath = strServiceCopy.Replace(BaseEntity + "Service.cs", NewEntity + "Service.cs");
                        CreateClone(strServiceCopy, FileNewpath, BaseEntity, NewEntity);
                    }
                    else
                    {
                        FileNewpath = txtblkService.Text + "\\" + NewEntity + "Service.cs";
                        CreateClone(strServiceCopy, FileNewpath, BaseEntity, NewEntity);
                    }

                    //Set the new file name - For Repository Mock
                    FileNewpath = strRepositoryCopy.Replace(BaseEntity + "RepositoryMock.cs", NewEntity + "RepositoryMock.cs");
                    CreateClone(strRepositoryCopy, FileNewpath, BaseEntity, NewEntity);

                    //Set the new file name - For Controller Test
                    FileNewpath = strControllerTestCopy.Replace(BaseEntity + "ControllerTest.cs", NewEntity + "ControllerTest.cs");
                    CreateClone(strControllerTestCopy, FileNewpath, BaseEntity, NewEntity);

                    //Set the new file name - For Service Test
                    FileNewpath = strServiceTestCopy.Replace(BaseEntity + "ServiceTest.cs", NewEntity + "ServiceTest.cs");
                    CreateClone(strServiceTestCopy, FileNewpath, BaseEntity, NewEntity);

                }
                else
                {
                    MessageBox.Show("Enter both BaseEntity and NewEntity ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on btnclone_Click" + ex);
            }


        }

        private void CreateClone(string copyof, string FileNewpath, string BaseEntity, string NewEntity)
        {
            try
            {
                //Clone file with New name
                File.Copy(copyof, FileNewpath);

                string text = File.ReadAllText(FileNewpath);
                text = text.Replace(BaseEntity, NewEntity);

                text = text.Replace(Char.ToLowerInvariant(BaseEntity[0]) + BaseEntity.Substring(1),
                                    Char.ToLowerInvariant(NewEntity[0]) + NewEntity.Substring(1));

                File.WriteAllText(FileNewpath, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on CreateClone" + ex);
            }
        }

        private string GetController(string BasePath, string BaseEntity)
        {
            try
            {
                List<string> lstFileExistPath = CheckIfFileExists(BasePath, BaseEntity + "Controller.cs");
                string FileExistPath = lstFileExistPath[0];
                if (FileExistPath != null)
                {
                    txtblkController.Text = "";
                    txtblkController.Text = FileExistPath;
                    strControllerCopy = FileExistPath;
                    lblController.Content = "Found";
                    return FileExistPath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetController" + ex);
                return ex.Message;
            }
        }

        private string GetService(string BasePath, string BaseEntity)
        {
            try
            {
                List<string> lstFileExistPath = CheckIfFileExists(BasePath, BaseEntity + "Service.cs");
                string FileExistPath = lstFileExistPath[0];
                if (FileExistPath != null)
                {
                    txtblkService.Text = "";
                    txtblkService.Text = FileExistPath;
                    strServiceCopy = FileExistPath;
                    lblService.Content = "Found";
                    return FileExistPath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetService" + ex);
                return ex.Message;
            }
        }

        private string GetRepository(string BasePath, string BaseEntity)
        {
            try
            {
                List<string> lstFileExistPath = CheckIfFileExists(BasePath, BaseEntity + "RepositoryMock.cs");
                string FileExistPath = lstFileExistPath[0];
                if (FileExistPath != null)
                {
                    txtblkRepository.Text = "";
                    txtblkRepository.Text = FileExistPath;
                    strRepositoryCopy = FileExistPath;
                    lblRepository.Content = "Found";
                    return FileExistPath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetRepository" + ex);
                return ex.Message;
            }
        }
        private string GetControllerTest(string BasePath, string BaseEntity)
        {
            try
            {
                List<string> lstFileExistPath = CheckIfFileExists(BasePath, BaseEntity + "ControllerTest.cs");
                string FileExistPath = lstFileExistPath[0];
                if (FileExistPath != null)
                {
                    txtbxControllerTest.Text = "";
                    txtbxControllerTest.Text = FileExistPath;
                    strControllerTestCopy = FileExistPath;
                    lblControllerTest.Content = "Found";
                    return FileExistPath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetControllerTest" + ex);
                return ex.Message;
            }
        }
        private string GetServiceTest(string BasePath, string BaseEntity)
        {
            try
            {
                List<string> lstFileExistPath = CheckIfFileExists(BasePath, BaseEntity + "ServiceTest.cs");
                string FileExistPath = lstFileExistPath[0];
                if (FileExistPath != null)
                {
                    txtbxServiceTest.Text = "";
                    txtbxServiceTest.Text = FileExistPath;
                    strServiceTestCopy = FileExistPath;
                    lblServiceTest.Content = "Found";
                    return FileExistPath;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetServiceTest" + ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// CheckIfFileExists
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<string> CheckIfFileExists(string directory, string fileName)
        {
            List<string> lstdir = new List<string>();
            var exists = false;
            var fileNameToCheck = System.IO.Path.Combine(directory, fileName);
            if (Directory.Exists(directory))
            {
                //check directory for file
                exists = Directory.GetFiles(directory).Any(x => x.Equals(fileNameToCheck, StringComparison.OrdinalIgnoreCase));

                //check subdirectories for file
                if (!exists)
                {
                    foreach (var dir in Directory.GetFiles(directory, "*" + fileName, SearchOption.AllDirectories).Select(System.IO.Path.GetFullPath))
                    {
                        //return dir;
                        lstdir.Add(dir);
                    }
                }
            }
            return lstdir;
        }

        /// <summary>
        /// Clone IOC config button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfigIOC_Click(object sender, RoutedEventArgs e)
        {
            var BasePath = txtblkBasePath.Text;
            var BaseEntity = txtblkBaseEntity.Text;

            //Add this line in IocContainerConfiguration.cs in API proj
            //services.AddScoped<IOrganizationBaseService, OrganizationBaseService>();
            var NewEntity = txtblkNewEntity.Text;

            List<string> lstFileExistPath = CheckIfFileExists(BasePath, "IocContainerConfiguration.cs");
            string FileExistPath = lstFileExistPath[1];

            Console.WriteLine(FileExistPath);

            //Added Dependancy in ConfigureService-----------------------------------------------------------------------
            var linetosearch = "            services.AddScoped<I" + BaseEntity + "Service, " + BaseEntity + "Service>();";
            string strIOCRegistration = "            services.AddScoped<I" + NewEntity + "Service, " + NewEntity + "Service>();";
            insertLineToSimpleFile(FileExistPath, linetosearch, strIOCRegistration);
            //-----------------------------------------------------------------------------------------------------------

            // Added line in ConfigureInMemoryProvide function in IOCContainerConfiguration-------------------------------
            string strIOCInMemoryRegistration =
            "           services.AddSingleton<I" + BaseEntity + "Repository>(x =>" +
            "{" +
            "    var listOf" + BaseEntity + "Details = Builder<" + BaseEntity + ">.CreateListOfSize(10).All().Build().ToList();" +
            "    return new " + BaseEntity + "RepositoryMock(listOf" + BaseEntity + "Details);" +
            "}); ";
            var ConfigInMemoryLine = "           services.AddSingleton<IOrganizationBaseRepository>(x =>";
            insertLineToSimpleFile(FileExistPath, ConfigInMemoryLine, strIOCInMemoryRegistration);
            //-----------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// Add a new line at a specific position in a simple file        
        /// </summary>
        /// <param name="fileName">Complete file path</param>
        /// <param name="lineToSearch">Line to search in the file (first occurrence)</param>
        /// <param name="lineToAdd">Line to be added</param>
        /// <param name="aboveBelow">insert above(false) or below(true) of the search line. Default: above </param>
        public void insertLineToSimpleFile(string fileName, string lineToSearch, string lineToAdd, bool aboveBelow = true)
        {
            var txtLines = File.ReadAllLines(fileName).ToList();
            //var result = string.Join(",", txtLines.ToArray());
            //Match m = Regex.Match(result, lineToSearch);

            //int index = m.Index;//aboveBelow ? txtLines.IndexOf(lineToSearch) + 1 : txtLines.IndexOf(lineToSearch);
            int index = aboveBelow ? txtLines.IndexOf(lineToSearch) + 1 : txtLines.IndexOf(lineToSearch);
            if (index > 0)
            {
                txtLines.Insert(index, lineToAdd);
                File.WriteAllLines(fileName, txtLines);
            }
        }

    }
}
