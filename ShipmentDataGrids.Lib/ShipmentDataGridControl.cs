using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
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
        Columns _columns;

        private const string ConfigFile = @"ConfigArm.json";

        #endregion

        public ShipmentDataGridControl()
        {
            InitializeComponent();

            string cfgPath = null;
            var dir1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string dir2 = @"C:\Project\Config\";

            var path1 = Path.Combine(dir1 ?? string.Empty, ConfigFile);
            var path2 = Path.Combine(dir2, ConfigFile);


            // Обертка в try-catch позволяет отловить ошибки, когда с библиотекой в Tia Portal
            try
            {
                if (File.Exists(ConfigFile))
                {
                    cfgPath = ConfigFile;
                }
                else if (File.Exists(path1))
                {
                    cfgPath = path1;
                }
                else if (File.Exists(path2))
                {
                    cfgPath = path2;
                }

                var config = CommonTools.GetConfig(cfgPath);

                if (config != null)
                {
                    try
                    {
                        _dbService = new DbService(config);
                        _columns = config.Columns;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show($"Не удалось загрузить файл: ${cfgPath}");
                }

                dataGridView1.AllowUserToAddRows = false;
                panelFilter.Controls[0].Focus();
                LockDateTimePickers();

                dateTimePickerBeginDate.Value = DateTime.Now;
                dateTimePickerBeginTime.Value = DateTime.Now;
                dateTimePickerEndDate.Value = DateTime.Now;
                dateTimePickerEndTime.Value = DateTime.Now;
            }
            catch (AggregateException ex)
            {

                string msg = "";
                foreach (var errInner in ex.InnerExceptions)
                {
                    msg += errInner + "\n";
                }
                MessageBox.Show(msg);
            }


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

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {

                if (column.Name != null && column.Name.IndexOf("Id", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.Id;

                if (column.Name != null && column.Name.IndexOf("Ts", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.Ts;

                if (column.Name != null && column.Name.IndexOf("PostName", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.PostName;

                if (column.Name != null && column.Name.IndexOf("ProductName", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.ProductName;

                if (column.Name != null && column.Name.IndexOf("TankName", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.TankName;

                if (column.Name != null && column.Name.IndexOf("TimeBegin", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.TimeBegin;

                if (column.Name != null && column.Name.IndexOf("TimeEnd", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.TimeEnd;

                if (column.Name != null && column.Name.IndexOf("SetPoint", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.SetPoint;

                if (column.Name != null && column.Name.IndexOf("ResultMain", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.ResultMain;

                if (column.Name != null && column.Name.IndexOf("UnitMain", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.UnitMain;

                if (column.Name != null && column.Name.IndexOf("ResultSecondary", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.ResultSecondary;

                if (column.Name != null && column.Name.IndexOf("UnitSecondary", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.UnitSecondary;

                if (column.Name != null && column.Name.IndexOf("Density", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.Density;

                if (column.Name != null && column.Name.IndexOf("Temperature", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.Temperature;

                if (column.Name != null && column.Name.IndexOf("FinalStatus", StringComparison.Ordinal) >= 0)
                    column.Visible = _columns.FinalStatus;

                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.Resizable = DataGridViewTriState.True;

                // Устанавливаем ширину строк "Время начала" и "Время окончания", чтобы влезло все
                if (column.Name != null && ((column.Name.IndexOf("TimeEnd", StringComparison.Ordinal) >= 0) ||
                                            column.Name.IndexOf("TimeBegin", StringComparison.Ordinal) >= 0))
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
                    FileName = $"Shipment_{DateTime.Now:yyyy-MM-dd_HH_mm_ss}"
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
                                if (dataGridView1.Columns[i].Visible) {
                                    columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ";";
                                }
                                
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    if (dataGridView1.Columns[j].Visible)
                                    {
                                        outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ";";
                                    }
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
            if (sender is RadioButton radioButton && radioButton.Checked)
            {

                switch (radioButton.Name)
                {
                    case "radioCustom":
                        UnlockDateTimePicker();
                        break;

                    default:
                        ReFillDataGrid(panelFilter.Controls);
                        break;
                }

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
