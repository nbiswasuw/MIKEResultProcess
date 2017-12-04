using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DHI.Generic.MikeZero.DFS;
using System.IO;
using DHI.Generic.MikeZero;
using System.Diagnostics;

namespace HDResultProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string fileName = "";
        List<DateTime> dfsDate = new List<DateTime>();
        List<string> WLItems = new List<string>();
        List<string> QItems = new List<string>();
        string dfs0Path = "";
        int noTimeSteps = 0;
        float[,] dfsData = new float[25000, 10000];

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            btnDFS0Fromlist.Visible = true;
            btnLoadHD.Visible = true;
            comboBox1.Visible = true;
            label2.Visible = false;
            label1.Visible = true;
            btnSingleDFS0.Visible = true;
            btnLoadNAM.Visible = false;
            btnNAM.Visible = false;
        }
        private void btnNAM_Click(object sender, EventArgs e)
        {
            btnDFS0Fromlist.Visible = false;
            btnLoadHD.Visible = false;
            comboBox1.Visible = false;
            label2.Visible = false;
            label1.Visible = false;
            btnSingleDFS0.Visible = false;
            btnLoadNAM.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The tool is for MIKE HD Result File processing. First Load a file, it will generate all available Items. From Drop-down menu, please select an Item or you can process dfs0 from file. Maximum Node Number = 9000 and Maximum Timestep is 10000. \nHave a nice computation.\n-Nishan Kumar Biswas");
        }
        private void btnLoadHD_Click(object sender, EventArgs e)
        {
            try
            {
                label2.Visible = true;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Mike HD Result Files|*.RES11";

                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    fileName = dialog.FileName;
                }

                ProcessStartInfo start = new ProcessStartInfo();
                Process exeProcess = new Process();
                start.FileName = @"C:\Program Files\DHI\2014\bin\res11read.exe";
                start.Arguments = "-xy " + fileName + " " + fileName.Substring(0, fileName.Length - 6) + "_xy.txt";
                exeProcess = Process.Start(start);
                start.CreateNoWindow = true;
                exeProcess.WaitForExit();

                string[] riverChainageFile = File.ReadAllLines(fileName.Substring(0, fileName.Length - 6) + "_xy.txt");
                char[] charSeparators = new char[] { ' ' };

                StringBuilder sb = new StringBuilder();
                for (int i = 19; i < riverChainageFile.Length - 3; i++)
                {
                    var texts = riverChainageFile[i].Substring(24, 140).Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    if (texts[2] == "2")
                    {
                        QItems.Add("Q," + texts[0] + "," + texts[1]);
                    }
                    else if (texts[2] == "0" || texts[2] == "1")
                    {
                        WLItems.Add("WL," + texts[0] + "," + texts[1]);
                    }
                }
                for (int i = 0; i < WLItems.Count; i++)
                {
                    sb.AppendLine(WLItems[i]);
                    comboBox1.Items.Add(WLItems[i]);
                }

                for (int i = 0; i < QItems.Count; i++)
                {
                    sb.AppendLine(QItems[i]);
                    comboBox1.Items.Add(QItems[i]);
                }
                File.Delete(fileName.Substring(0, fileName.Length - 6) + "_xy.txt");
                File.WriteAllText(fileName.Substring(0, fileName.Length - 6) + "_xy.txt", sb.ToString());
                IDfsFile resFile = DfsFileFactory.DfsGenericOpen(fileName);
                DateTime[] date = resFile.FileInfo.TimeAxis.GetDateTimes();
                DateTime startDate = date[0];
                IDfsFileInfo resfileInfo = resFile.FileInfo;
                IDfsItemData<float> data;
                noTimeSteps = resfileInfo.TimeAxis.NumberOfTimeSteps;

                int cx = 0;
                for (int j = 0; j < resFile.ItemInfo.Count; j++)
                {
                    IDfsSimpleDynamicItemInfo dynamicItemInfo = resFile.ItemInfo[j];
                    data = (IDfsItemData<float>)resFile.ReadItemTimeStep(j + 1, 0);
                    cx = cx + dynamicItemInfo.ElementCount;
                }
                MessageBox.Show(cx.ToString());

                for (int i = 0; i < noTimeSteps; i++)
                {
                    dfsDate.Add(startDate.AddHours(resFile.ReadItemTimeStep(1, i).Time));
                }

                for (int i = 0; i < noTimeSteps; i++)
                {
                    int counter = 0;
                    int totalNode = 0;

                    for (int j = 0; j < resFile.ItemInfo.Count; j++)
                    {
                        IDfsSimpleDynamicItemInfo dynamicItemInfo = resFile.ItemInfo[j];
                        data = (IDfsItemData<float>)resFile.ReadItemTimeStep(j + 1, i);
                        counter = dynamicItemInfo.ElementCount;
                        for (int z = 0; z < counter; z++)
                        {
                            if (totalNode < comboBox1.Items.Count)
                            {
                                dfsData[i, totalNode] = (Convert.ToSingle(data.Data[z]));
                                totalNode = totalNode + 1;
                            }
                            else { break; }
                        }
                    }
                }
                var filepath = fileName.Split('\\');
                dfs0Path = filepath[0];
                for (int i = 1; i < filepath.Length - 1; i++)
                {
                    dfs0Path = dfs0Path + @"\" + filepath[i];
                }
                label2.Text = "Loaded successfully.";
            }
            catch (Exception error)
            {
                MessageBox.Show("File have not loaded. Error: " + error.Message);
            }
        }
        private void btnLoadNAM_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Mike NAM Result Files|*.RES11";

            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                fileName = dialog.FileName;
            }


            var filepath = fileName.Split('\\');
            dfs0Path = filepath[0];
            for (int i = 1; i < filepath.Length - 1; i++)
            {
                dfs0Path = dfs0Path + @"\" + filepath[i];
            }

            IDfsFile resFile = DfsFileFactory.DfsGenericOpenEdit(fileName);
            IDfsFileInfo resfileInfo = resFile.FileInfo;
            DateTime[] date = resFile.FileInfo.TimeAxis.GetDateTimes();
            DateTime startDate = date[0];

            IDfsItemData<float> data;
            int noTimeSteps = resfileInfo.TimeAxis.NumberOfTimeSteps;
            float[] values = new float[noTimeSteps];
            for (int i = 0; i < noTimeSteps; i++)
            {
                dfsDate.Add(startDate.AddHours(resFile.ReadItemTimeStep(1, i).Time));
            }

            for (int j = 0; j < resFile.ItemInfo.Count; j++)
            {
                IDfsSimpleDynamicItemInfo dynamicItemInfo = resFile.ItemInfo[j];
                string nameOftDynamicItem = dynamicItemInfo.Name;
                string checkname = nameOftDynamicItem.Substring(0, 6);
                if (checkname == "RunOff")
                {
                    string filename = dfs0Path + @"\" + nameOftDynamicItem + ".dfs0";
                    DfsFactory factory = new DfsFactory();
                    DfsBuilder filecreator = DfsBuilder.Create(nameOftDynamicItem, nameOftDynamicItem, 2014);
                    filecreator.SetDataType(1);
                    filecreator.SetGeographicalProjection(factory.CreateProjectionUndefined());
                    //filecreator.SetTemporalAxis(factory.CreateTemporalEqCalendarAxis(eumUnit.eumUsec, new DateTime(2010, 01, 01, 06, 00, 00), 0, 10800));
                    filecreator.SetTemporalAxis(factory.CreateTemporalNonEqCalendarAxis(eumUnit.eumUsec, new DateTime(dfsDate[0].Year, dfsDate[0].Month, dfsDate[0].Day, dfsDate[0].Hour, dfsDate[0].Minute, dfsDate[0].Second)));
                    filecreator.SetItemStatisticsType(StatType.RegularStat);
                    DfsDynamicItemBuilder item = filecreator.CreateDynamicItemBuilder();
                    item.Set(nameOftDynamicItem, eumQuantity.Create(eumItem.eumIDischarge, eumUnit.eumUm3PerSec), DfsSimpleType.Float);
                    item.SetValueType(DataValueType.Instantaneous);
                    item.SetAxis(factory.CreateAxisEqD0());
                    item.SetReferenceCoordinates(1f, 2f, 3f);
                    filecreator.AddDynamicItem(item.GetDynamicItemInfo());

                    filecreator.CreateFile(filename);
                    IDfsFile file = filecreator.GetFile();

                    for (int i = 0; i < noTimeSteps; i++)
                    {
                        data = (IDfsItemData<float>)resFile.ReadItemTimeStep(j + 1, i);
                        values[i] = Convert.ToSingle(data.Data[0]);
                        file.WriteItemTimeStepNext((dfsDate[i] - dfsDate[0]).TotalSeconds, new float[] { values[i] });
                    }
                    file.Close();
                }
            }
        }
        private void btnSingleDFS0_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileName == "")
                {
                    MessageBox.Show("No files have been selected for processing...\nPlease Load a file first.");
                }
                else
                {
                    string itemType = comboBox1.SelectedItem.ToString().Substring(0, 2);

                    if (itemType == "WL")
                    {
                        string element = comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().Length - 4);
                        DfsFactory factory = new DfsFactory();
                        string filename = dfs0Path + @"\" + element + ".dfs0";
                        DfsBuilder filecreator = DfsBuilder.Create(element, element, 2012);
                        filecreator.SetDataType(1);
                        filecreator.SetGeographicalProjection(factory.CreateProjectionUndefined());
                        filecreator.SetTemporalAxis(factory.CreateTemporalNonEqCalendarAxis(eumUnit.eumUsec, new DateTime(dfsDate[0].Year, dfsDate[0].Month, dfsDate[0].Day, dfsDate[0].Hour, dfsDate[0].Minute, dfsDate[0].Second)));
                        filecreator.SetItemStatisticsType(StatType.RegularStat);
                        DfsDynamicItemBuilder item = filecreator.CreateDynamicItemBuilder();
                        item.Set(element, eumQuantity.Create(eumItem.eumIWaterLevel, eumUnit.eumUmeter), DfsSimpleType.Float);
                        item.SetValueType(DataValueType.Instantaneous);
                        item.SetAxis(factory.CreateAxisEqD0());
                        item.SetReferenceCoordinates(1f, 2f, 3f);
                        filecreator.AddDynamicItem(item.GetDynamicItemInfo());

                        filecreator.CreateFile(filename);
                        IDfsFile file = filecreator.GetFile();
                        IDfsFileInfo fileinfo = file.FileInfo;

                        for (int j = 0; j < dfsDate.Count; j++)
                        {
                            file.WriteItemTimeStepNext((dfsDate[j] - dfsDate[0]).TotalSeconds, new float[] { dfsData[j, comboBox1.SelectedIndex] });
                        }
                        file.Close();
                    }
                    else if (itemType == "Q,")
                    {
                        string element = comboBox1.SelectedItem.ToString();
                        DfsFactory factory = new DfsFactory();
                        string filename = dfs0Path + @"\" + element + ".dfs0";
                        DfsBuilder filecreator = DfsBuilder.Create(element, element, 2014);
                        filecreator.SetDataType(1);
                        filecreator.SetGeographicalProjection(factory.CreateProjectionUndefined());
                        filecreator.SetTemporalAxis(factory.CreateTemporalNonEqCalendarAxis(eumUnit.eumUsec, new DateTime(dfsDate[0].Year, dfsDate[0].Month, dfsDate[0].Day, dfsDate[0].Hour, dfsDate[0].Minute, dfsDate[0].Second)));
                        filecreator.SetItemStatisticsType(StatType.RegularStat);
                        DfsDynamicItemBuilder item = filecreator.CreateDynamicItemBuilder();
                        item.Set(element, eumQuantity.Create(eumItem.eumIDischarge, eumUnit.eumUm3PerSec), DfsSimpleType.Float);
                        item.SetValueType(DataValueType.Instantaneous);
                        item.SetAxis(factory.CreateAxisEqD0());
                        item.SetReferenceCoordinates(1f, 2f, 3f);
                        filecreator.AddDynamicItem(item.GetDynamicItemInfo());

                        filecreator.CreateFile(filename);
                        IDfsFile file = filecreator.GetFile();
                        IDfsFileInfo fileinfo = file.FileInfo;

                        for (int j = 0; j < dfsDate.Count; j++)
                        {
                            file.WriteItemTimeStepNext((dfsDate[j] - dfsDate[0]).TotalSeconds, new float[] { dfsData[j, comboBox1.SelectedIndex] });
                        }
                        file.Close();
                    }

                    MessageBox.Show("Result file processed suceesssfully.");
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("HD Model Result files cannot be processed due to an error. Error: " + error.Message);
            }
        }
        private void btnDFS0Fromlist_Click(object sender, EventArgs e)
        {
            if (fileName == "")
            {
                MessageBox.Show("No files have been selected for processing...\nPlease Load a file first.");
            }
            else 
            {
                string[] requiredDFS0File = File.ReadAllLines((fileName.Substring(0, fileName.Length - 6) + ".txt"));
                string[] availableDFS0 = File.ReadAllLines((fileName.Substring(0, fileName.Length - 6) + "_xy.txt"));
                foreach (string element in requiredDFS0File)
                {
                    for (int i = 0; i < availableDFS0.Length; i++)
                    {
                        if (element == availableDFS0[i])
                        {
                            string itemType = element.Substring(0, 2);

                            if (itemType == "WL")
                            {
                                DfsFactory factory = new DfsFactory();
                                string filename = dfs0Path + @"\" + element + ".dfs0";
                                DfsBuilder filecreator = DfsBuilder.Create(element, element, 2012);
                                filecreator.SetDataType(1);
                                filecreator.SetGeographicalProjection(factory.CreateProjectionUndefined());
                                filecreator.SetTemporalAxis(factory.CreateTemporalNonEqCalendarAxis(eumUnit.eumUsec, new DateTime(dfsDate[0].Year, dfsDate[0].Month, dfsDate[0].Day, dfsDate[0].Hour, dfsDate[0].Minute, dfsDate[0].Second)));
                                filecreator.SetItemStatisticsType(StatType.RegularStat);
                                DfsDynamicItemBuilder item = filecreator.CreateDynamicItemBuilder();
                                item.Set(element, eumQuantity.Create(eumItem.eumIWaterLevel, eumUnit.eumUmeter), DfsSimpleType.Float);
                                item.SetValueType(DataValueType.Instantaneous);
                                item.SetAxis(factory.CreateAxisEqD0());
                                item.SetReferenceCoordinates(1f, 2f, 3f);
                                filecreator.AddDynamicItem(item.GetDynamicItemInfo());

                                filecreator.CreateFile(filename);
                                IDfsFile file = filecreator.GetFile();
                                IDfsFileInfo fileinfo = file.FileInfo;

                                for (int j = 0; j < dfsDate.Count; j++)
                                {
                                    file.WriteItemTimeStepNext((dfsDate[j] - dfsDate[0]).TotalSeconds, new float[] { dfsData[j, i] });
                                }
                                file.Close();
                            }
                            else if (itemType == "Q,")
                            {
                                DfsFactory factory = new DfsFactory();
                                string filename = dfs0Path + @"\" + element + ".dfs0";
                                DfsBuilder filecreator = DfsBuilder.Create(element, element, 2014);
                                filecreator.SetDataType(1);
                                filecreator.SetGeographicalProjection(factory.CreateProjectionUndefined());
                                filecreator.SetTemporalAxis(factory.CreateTemporalNonEqCalendarAxis(eumUnit.eumUsec, new DateTime(dfsDate[0].Year, dfsDate[0].Month, dfsDate[0].Day, dfsDate[0].Hour, dfsDate[0].Minute, dfsDate[0].Second)));
                                filecreator.SetItemStatisticsType(StatType.RegularStat);
                                DfsDynamicItemBuilder item = filecreator.CreateDynamicItemBuilder();
                                item.Set(element, eumQuantity.Create(eumItem.eumIDischarge, eumUnit.eumUm3PerSec), DfsSimpleType.Float);
                                item.SetValueType(DataValueType.Instantaneous);
                                item.SetAxis(factory.CreateAxisEqD0());
                                item.SetReferenceCoordinates(1f, 2f, 3f);
                                filecreator.AddDynamicItem(item.GetDynamicItemInfo());

                                filecreator.CreateFile(filename);
                                IDfsFile file = filecreator.GetFile();
                                IDfsFileInfo fileinfo = file.FileInfo;

                                for (int j = 0; j < dfsDate.Count; j++)
                                {
                                    file.WriteItemTimeStepNext((dfsDate[j] - dfsDate[0]).TotalSeconds, new float[] { dfsData[j, i] });
                                }
                                file.Close();
                            }
                        }
                    }
                }
                MessageBox.Show("Result file processed suceesssfully.");
            }
        }
    }
}
