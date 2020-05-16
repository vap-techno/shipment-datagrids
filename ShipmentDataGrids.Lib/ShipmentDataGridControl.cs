﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ShipmentDataGrids.Lib.Services;
using ShipmentDataGrids.Lib.Interfaces;
using ShipmentDataGrids.Lib.Models;
using ShipmentDataGrids.Lib.Common;

namespace ShipmentDataGrids.Lib
{
    public partial class ShipmentDataGridControl : UserControl
    {

        #region Fields

        // Сервис работы с БД
        DbService _dbService;

        private const string ConfigFile = @"ConfigArm.json";

        #endregion

        public ShipmentDataGridControl()
        {
            InitializeComponent();

            var config = CommonTools.GetConfig(ConfigFile);
            _dbService = new DbService(config);
            
            dataGridView1.AllowUserToAddRows = false;
            panelFilter.Controls[0].Focus();
            LockDateTimePickers();

            dateTimePickerBeginDate.Value = DateTime.Now;
            dateTimePickerBeginTime.Value = DateTime.Now;
            dateTimePickerEndDate.Value = DateTime.Now;
            dateTimePickerEndTime.Value = DateTime.Now;
        }

        #region Methods

        /// <summary>
        /// Заполняет DataGrid в зависимости от выбранного radiobutton в коллекции
        /// </summary>
        /// <param name="controls"> Список контролов в панели, бросить сюда Panel.Controls </param>
        private void ReFillDataGrid(ControlCollection controls)
        {
            
            // Определяем отмеченный radiobutton и обновляем dataGrid
            RadioButton _rb = controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);

            if (_rb == null) return;

            List<IShipment> lst = null;
            switch (_rb.Name)
            {
                case "radioDay":
                    lst = _dbService.GetShipmentsLastDay();
                    break;
                case "radioWeek":
                    lst = _dbService.GetShipmentsLastWeek();
                    break;
                case "radioMonth":
                    lst = _dbService.GetShipmentsLastMonth();
                    break;
                case "radioYear":
                    lst = _dbService.GetShipmentsLastYear();
                    break;
                case "radioAll":
                    lst = _dbService.GetShipments();
                    break;
                case "radioCustom":

                    var beginDate = dateTimePickerBeginDate.Value.Date;
                    var beginTime = dateTimePickerBeginTime.Value.TimeOfDay;
                    var customDateBegin = beginDate + beginTime;

                    var endDate = dateTimePickerEndDate.Value.Date;
                    var endTime = dateTimePickerEndTime.Value.TimeOfDay;
                    var customDateEnd = endDate + endTime;


                    lst = _dbService.GetShipmentsInRange(customDateBegin, customDateEnd);
                    break;
            }

            SortableBindingList<IShipment> srtlst = new SortableBindingList<IShipment>(lst);

            dataGridView1.DataSource = srtlst;
            FormatDataGridView(dataGridView1);

        }

        /// <summary>
        /// Отформатировать вывод таблицы
        /// </summary>
        /// <param name="dataGridView"></param>
        private void FormatDataGridView(DataGridView dataGridView)
        {

            dataGridView1.Columns[0].DataPropertyName = "Id";
            dataGridView1.Columns[1].DataPropertyName = "Ts";
            dataGridView1.Columns[2].DataPropertyName = "PostName";
            dataGridView1.Columns[3].DataPropertyName = "TimeBegin";
            dataGridView1.Columns[4].DataPropertyName = "TimeEnd";
            dataGridView1.Columns[5].DataPropertyName = "SetPoint";
            dataGridView1.Columns[6].DataPropertyName = "ResultWeight";
            dataGridView1.Columns[7].DataPropertyName = "ResultVolume";
            dataGridView1.Columns[8].DataPropertyName = "Unit";
            dataGridView1.Columns[9].DataPropertyName = "Density";
            dataGridView1.Columns[10].DataPropertyName = "Temperature";
            dataGridView1.Columns[11].DataPropertyName = "ProductName";
            dataGridView1.Columns[12].DataPropertyName = "TankName";
            dataGridView1.Columns[13].DataPropertyName = "FinalStatus";

            dataGridView.Columns[0].HeaderText = "ID";
            dataGridView.Columns[1].HeaderText = "TS";
            dataGridView.Columns[2].HeaderText = "Пост";
            dataGridView.Columns[3].HeaderText = "Время начала";
            dataGridView.Columns[4].HeaderText = "Время окончания";
            dataGridView.Columns[5].HeaderText = "Задание";
            dataGridView.Columns[6].HeaderText = "Отгружено (масса)";
            dataGridView.Columns[7].HeaderText = "Отгружено (объем)";
            dataGridView.Columns[8].HeaderText = "Тип отгрузки";
            dataGridView.Columns[9].HeaderText = "Плотность";
            dataGridView.Columns[10].HeaderText = "Температура";
            dataGridView.Columns[11].HeaderText = "Продукт";
            dataGridView.Columns[12].HeaderText = "Цистерна";
            dataGridView.Columns[13].HeaderText = "Статус";

            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Visible = false;


            foreach (DataGridViewColumn column in dataGridView.Columns)
            {

                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.Resizable = DataGridViewTriState.True;

                // Устанавливаем ширину строк "Время начала" и "Время окончания", чтобы влезло все
                if ((column.Name.IndexOf("TimeEnd", StringComparison.Ordinal) >= 0) ||
                    column.Name.IndexOf("TimeBegin", StringComparison.Ordinal) >= 0)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }


            }
        }

        /// <summary>
        /// Блокирует элементы произвольной выборки
        /// </summary>
        private void LockDateTimePickers()
        {
            dateTimePickerBeginDate.Enabled = false;
            dateTimePickerBeginTime.Enabled = false;
            dateTimePickerEndDate.Enabled = false;
            dateTimePickerEndTime.Enabled = false;

            dateTimePickerBeginDate.Parent.Refresh();
        }

        /// <summary>
        /// Разблокирует элементы произвольной выборки
        /// </summary>
        private void UnlockDateTimePicker()
        {
            dateTimePickerBeginDate.Enabled = true;
            dateTimePickerBeginTime.Enabled = true;
            dateTimePickerEndDate.Enabled = true;
            dateTimePickerEndTime.Enabled = true;

            dateTimePickerBeginDate.Parent.Refresh();

        }

        /// <summary>
        /// Экспорт таблицы в CSV
        /// </summary>
        private void ExportToCsv()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FilterIndex = 2,
                    FileName = $"Shipment_{DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss")}"
                };
                
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Невозможно записать данные на этот диск" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ";";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ";";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Экспорт завершен", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Нечего экспортировать", "Info");
            }
        }


        #endregion

        #region Event Handlers

        /// <summary>
        /// Нажатие кнопки "Обновить" создает заново запрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ReFillDataGrid(panelFilter.Controls);
        }

        /// <summary>
        /// Обработка выбора временных фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            LockDateTimePickers();
            var radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {

                // NEW
                switch (radioButton.Name)
                {
                    case "radioCustom":
                        UnlockDateTimePicker();
                        break;

                    default:
                        ReFillDataGrid(panelFilter.Controls);
                        break;
                }

                // OLD
                //switch (radioButton.Name)
                //{
                //    case "radioDay":
                //        FillDataGrid(SqlDay);
                //        break;
                //    case "radioWeek":
                //        FillDataGrid(SqlWeek);
                //        break;
                //    case "radioMonth":
                //        FillDataGrid(SqlMonth);
                //        break;
                //    case "radioYear":
                //        FillDataGrid(SqlYear);

                //        break;
                //    case "radioAll":
                //        FillDataGrid(SqlAll);
                //        break;

                //    case "radioCustom":

                //        UnlockDateTimePicker();
                //        var qeury = GetQuerySqlStringCustom(_customDateBegin, _customDateEnd);
                //        FillDataGrid(qeury);
                //        break;

                //}
            }
        }

        private void dateTimePickerBegin_ValueChanged(object sender, EventArgs e)
        {
            ReFillDataGrid(panelFilter.Controls);
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            ReFillDataGrid(panelFilter.Controls);
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            //ExportToExcel();
            ExportToCsv();
        }


        //TODO: Разблокировать только при асинхронных запросах
        /// <summary>
        /// Через каждое срабатывание таймер обновляет запрос к БД и записывает в DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            ReFillDataGrid(panelFilter.Controls);
        }

        #endregion

    }

}
