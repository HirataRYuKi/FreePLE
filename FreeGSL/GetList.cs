using System.Collections.ObjectModel;
using static FreeGSL.MainPage;

namespace FreeGSL
{
    internal class GetList
    {
        public async Task<ObservableCollection<Info_App>> Getimport(string name)
        {
            ObservableCollection<Info_App> im = new ObservableCollection<Info_App>();
            await Task.Run(() =>
            {
                StreamReader sm = new StreamReader(name);
                string[] split = new string[5];
                while (sm.Peek() > -1)
                {
                    split = sm.ReadLine().Split(",");
                    im.Add(new Info_App { Name = split[0], Ver = split[1], Url = split[2] });
                }

            });
            return im;

        }
        public async Task<ObservableCollection<Info_App>> GetUninstallList()
        {
            ObservableCollection<string> ret = new ObservableCollection<string>();
            ObservableCollection<Info_App> Info = new ObservableCollection<Info_App>();

            await Task.Run(() =>
                {
                    string uninstall_path = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                    Microsoft.Win32.RegistryKey uninstall = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstall_path, false);
                    if (uninstall != null)
                    {
                        foreach (string subKey in uninstall.GetSubKeyNames())
                        {
                            string appName = null;
                            Microsoft.Win32.RegistryKey appkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(uninstall_path + "\\" + subKey, false);

                            if (appkey.GetValue("DisplayName") != null && appkey.GetValue("DisplayVersion") != null)
                            {
                                appName = $"{appkey.GetValue("DisplayName").ToString()},{appkey.GetValue("DisplayVersion").ToString()},なし";
                            }
                            else if (appkey.GetValue("DisplayName") != null && appkey.GetValue("DisplayVersion") == null)
                            {
                                appName = $"{appkey.GetValue("DisplayName").ToString()},なし,なし";

                            }
                            else if (appkey.GetValue("DisplayName") != null && appkey.GetValue("DisplayVersion") != null && appkey.GetValue("URLUpdateInfo") != null)
                            {
                                appName = $"{appkey.GetValue("DisplayName").ToString()},{appkey.GetValue("DisplayVersion").ToString()},{appkey.GetValue("URLUpdateInfo").ToString()}";

                            }
                            else if (appkey.GetValue("DisplayName") != null && appkey.GetValue("DisplayVersion") == null && appkey.GetValue("URLUpdateInfo") == null)
                            {
                                appName = $"{appkey.GetValue("DisplayName").ToString()},なし,なし";

                            }
                            else
                            {
                                appName = $"{subKey},なし,なし";
                            }


                            ret.Add(appName);
                        }
                    }
                });
            foreach (string s in ret)
            {
                string[] strings = s.Split(',');
                Info.Add(new Info_App { Name = strings[0], Ver = strings[1], Url = strings[2] });

                Console.WriteLine(s);
            }

            return Info;
        }
        public ObservableCollection<Info_App> RuntimeExclude(ObservableCollection<Info_App> apps)
        {
            ObservableCollection<Info_App> ret = new ObservableCollection<Info_App>();
            string[] RuntimeList = { "Runtime", "C++", "dotnet", "DotNet", ".NET", "Java", "java", "runtime", "Framework", "sdk", "Sdk", "SDK" };
            bool find = false;
            foreach (var item in apps)
            {
                find = false;
                foreach (var array in RuntimeList)
                {
                    if ((bool)(item as Info_App)?.Name.Contains(array))
                    {
                        find = true;
                    }
                }
                if (!find)
                {
                    ret.Add(new Info_App { Name = (item as Info_App)?.Name, Ver = (item as Info_App)?.Ver, Url = (item as Info_App)?.Url });
                }

            }
            return ret;
        }
    }
}
