using System.Diagnostics;

namespace FreeGSL;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
        CINI();
    }

    private int u = 0;

    /// <summary>
    /// ini�t�@�C�����쐬�iini�`���ł͂Ȃ��j
    /// </summary>
    private void CINI()
    {
        if (!File.Exists(@"C:\FreeGSL\Settings.ini"))
        {
            Directory.CreateDirectory(@"C:\FreeGSL");
            using (File.Create(@"C:\FreeGSL\Settings.ini")) ;

        }
        else
        {
            string[] Setting = new string[3];
            string[] Runtime;
            string[] EU;
            string[] update;

            using (StreamReader sr = new StreamReader(@"C:\FreeGSL\Settings.ini"))
            {
                int i = 0;
                // �ǂݍ��݂ł��镶�����Ȃ��Ȃ�܂ŌJ��Ԃ�
                while (sr.Peek() >= 0)
                {
                    // �t�@�C���� 1 �s���ǂݍ���
                    string stBuffer = sr.ReadLine();
                    // �ǂݍ��񂾂��̂�ǉ��Ŋi�[����
                    Setting[i] = stBuffer;
                    i++;
                }
            }
            update = Setting[0].Split(',');
            Runtime = Setting[1].Split(',');
            EU = Setting[2].Split(","); 
            if (update[1] == "1")
            {
                Update.IsChecked = true;
            }
            else
            {
                Update.IsChecked = false;
            }

            if (Runtime[1] == "1")
            {
                runtime.IsChecked = true;
            }
            else
            {
                runtime.IsChecked = false;
            }

            if (EU[1] == "1")
            {
                Enable_Update.IsChecked = true;
            }
            else
            {
                Enable_Update.IsChecked = false;
            }

        }

    }
    /// <summary>
    /// �A�b�v�f�[�g��L���ɂ��邩�؂�ւ��ł���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Check_Update(object sender, CheckedChangedEventArgs e)
    {


    }
    private void Check_Source(object sender, CheckedChangedEventArgs e)
    {

    }
    private void Disable_Runtime(object sender, CheckedChangedEventArgs e)
    {

    }

    private void Enable_Update_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void Help_Clicked(object sender, EventArgs e)
    {
        ProcessStartInfo pi = new ProcessStartInfo()
        {
            FileName = "mailto:kd1319807@st.kobedenshi.ac.jp?subject=FreePLE�ɂ��Ă̂��₢���킹&body=�����ɂ��₢���킹���e�����������������B",
            UseShellExecute = true
        };
        Process.Start(pi);
    }
    /// <summary>
    /// �܂Ƃ߂Đݒ�������o��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        Save();
    }

    private void Save()
    {
        string all = $"Update,{(Update.IsChecked ? "1" : "0")}\n" + $"runtime,{(runtime.IsChecked ? "1" : "0")}\n" + $"Enable_Update,{(Enable_Update.IsChecked ? "1" : "0")}";
        using (StreamWriter sw = new StreamWriter(@"C:\FreeGSL\Settings.ini"))
        {
            sw.WriteLine(all);
        }
    }
    private void ContentPage_Unfocused(object sender, FocusEventArgs e)
    {
        Save();
        /*
     * �܂Ƃ߂ď����o��*/
    }
}

/*���@...
/*
�@�@�@�@�@�@/��Y_ -�@ �P �M -/��Y
�@�@�@�@�@�@�T�@�@�@�@�@�@�@�@�@ �@ '�M �A
�@�@�@�@ �^�@�@�@�@�@�@�@�@�@�@�@�@�@�@`�
�@�@�@�@���@�@�@�@�@ �@ �@ �@ �@ �@ �@ �@ �:,
�@�@�@/ �@ �@ __ �@ �@ __�@�@ �@ �@ �@ �@ �@ ',
�@ �@ |�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@}
�@�@�@', /:/: �� �A�A�@�� /:/:/�@�@�@�@�@�@;
�@�@ �@ �A �@ ,.:'�L:7^�N�L�R�@�@�@�@�@�@�@�@ /
�@ �@ �@ �___l:. : {:. :. :. :.}_�@�@�@�@�@�@�@i�L
�@�@�@�@�@ (__}�P�P�P�P }/�N�@�@�@�@�@;
�@�@ �@ �@ �@ }�Q�Q�Q�Q_}�M�N�N�L�@ �@ /_)
�@�@�@ �@ { {�@(__,��@�@�@���@�@_,,.����L
�@�@�@�@�@�R�R�@�@�M �--�](�R�m�A
*/


/* 2023/10/11
 * �Ƃ肠�����A�\�ʏ�̋@�\�͍l�����B.NET MAUI�͕Ȃ������c
 * WPF�A�v���ɂ��悤���l�������A�`�[������ł��g���̂Ŋ���Ă��������Ǝv��������.NET MAUI�ɂ����B
 */

/*
 * �u2�ʂ���_���Ȃ�ł��傤���v
 * �x�x�̌��w�ɍs���ĂӂƎv���o����*/

