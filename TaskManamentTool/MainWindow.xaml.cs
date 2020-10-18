using System;
using System.Linq;
using System.Windows;
using TaskManamentTool.Model;

namespace TaskManamentTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TmtDBContext tmtDBContext;
        
        public MainWindow()
        {
            InitializeComponent();

            // Step1: create an instance of DBContext
            tmtDBContext = new TmtDBContext();

            // Step2: get list of Status from DB
            var statuses = tmtDBContext.Statuses.ToList();

            // Step3: binding to UI, populate combo box
            foreach (var s in statuses)
            {
                cbo_Status.Items.Add(s);
            }

            RefreshDataGrid();
        }

        private async void Btn_Create_Click(object sender, RoutedEventArgs e)
        {
            if (cbo_Status.SelectedItem != null && !string.IsNullOrEmpty(tb_Task.Text))
            {
                //contruct a new Task intance based on form value:
                var newTask = new Model.Task(tb_Task.Text, (cbo_Status.SelectedItem as Model.Status).Id, dp_DueDate.SelectedDate.Value);
                // add to in-code DBContext, not to DB yet
                tmtDBContext.Tasks.Add(newTask);
                // this action add info to DB
                await tmtDBContext.SaveChangesAsync();

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Please input an invalid Task.");
            }
        }

        private void RefreshDataGrid()
        {
            // 1. Create a bindign obj
            var binding = new BindingSource();
            // 2. Query all Tasks from DBContext, using Linq
            // 1) Linq syntax:
            var queryTask = from t in tmtDBContext.Tasks
                            orderby t.DueDate descending
                            select new
                            {
                                t.Id,
                                TaskName = t.Name,
                                StatusName = t.Status.Name,
                                t.DueDate
                            };
            // 2) lambda extension method:
            var queryTask2 = tmtDBContext.Tasks
                            .OrderByDescending(t => t.DueDate)
                            .Select(t => new
                            {
                                t.Id,
                                TaskName = t.Name,
                                StatusName = t.Status.Name,
                                t.DueDate
                            }
                            );
            // 3. Set Linq query result to BindingSource datasource
            binding.DataSource = queryTask2.ToList();
            // 4. Set UI datasource to BindingSource datasource
            dg_Task.DataSource = binding;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var task2Delete = tmtDBContext.Tasks.Find(dg_Task.SelectedCells[0]);

            tmtDBContext.Tasks.Remove(task2Delete);

            tmtDBContext.SaveChanges();

            RefreshDataGrid();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetTaskInfo();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((string)BtnUpdate.Content == "Update")
            {
                //// find a reference variable
                //var task2Update = tmtDBContext.Tasks.Find(dg_Task.SelectedCells[0]);
                //// populate Task info cells with selected task in datagrid
                //tb_Task.Text = task2Update.Name;
                //dp_DueDate.SelectedDate = task2Update.DueDate;
                //cbo_Status.SelectedItem = task2Update.Status;

                var taskSelectedInGrid = dg_Task.SelectedCells;

                tb_Task.Text = taskSelectedInGrid[1].ToString();
                dp_DueDate = (DateTime)taskSelectedInGrid[3];

                foreach (Model.Status status in cbo_Status.Items)
                {
                    if (status.Name == taskSelectedInGrid[2].ToString())
                        cbo_Status.SelectedItem = status;
                }
                // flip BtnUpdate content
                BtnUpdate.Content = "Save";
            }


            if ((string)BtnUpdate.Content == "Save")
            {
                // find a reference variable
                var task2Update = tmtDBContext.Tasks.Find(dg_Task.SelectedCells[0]);
                // update properties
                task2Update.Name = tb_Task.Text;
                task2Update.StatusId = (cbo_Status.SelectedItem as Status).Id;
                task2Update.DueDate = dp_DueDate.SelectedDate.Value;

                tmtDBContext.SaveChanges();

                RefreshDataGrid();

                //BtnUpdate.Content = "Update";
                ResetTaskInfo();
            }

        }

        private void ResetTaskInfo()
        {
            BtnUpdate.Content = "Update";
            tb_Task.Text = "";
            dp_DueDate.SelectedDate = DateTime.Today;
            cbo_Status.Text = "Please select...";
        }
    }
}
