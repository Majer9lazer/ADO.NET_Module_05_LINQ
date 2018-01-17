using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

namespace ADO.NETLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _connstring = "Data Source=192.168.111.107; Initial Catalog=CRCMS_new; User Id=sa; Password=Mc123456";
        CRCMS_new db = new CRCMS_new();
        public MainWindow()
        {
            InitializeComponent();


        }

        private void GetResult_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(_connstring);
            SqlDataAdapter da = new SqlDataAdapter("select * from dic_Model", con);
            DataSet ds = new DataSet();
            da.TableMappings.Add("Table", "dic_Model");
            da.Fill(ds);
            IEnumerable<DataRow> res = ds.Tables["dic_Model"].AsEnumerable();


        }

        private void GetDocument_Click(object sender, RoutedEventArgs e)
        {
            //DocumentList.ItemsSource = (from doc in db.Documents
            //                            where doc.SmcsCode == "1000"
            //                            group doc by doc.CustomerId).
            //                            ToList();
            //int count = (from doc in db.Documents
            //             where doc.SmcsCode == "1000"
            //             select doc).Count();
            try
            {

                var res = db.Timers.
                    Where(e1=>e1.DateFinish!=null).
                    Where(w => db.Documents.Where
                    (ww => ww.ArrivalMonth != null).
                    Select(ss => ss.DocumentId).Count() > 0)
                    .Where(w => w.UserId != 0 && w.AreaId == 19)
                    .Select(s => new
                    {
                        s.UserId,
                        DocumentNumber = db.Documents.FirstOrDefault(f => f.DocumentId == s.DocumentId).DocumentNumber,
                        s.AreaId,
                        s.DateStart,
                        s.DateFinish,
                        DurationInSeconds = DbFunctions.DiffMinutes(s.DateStart.Value,s.DateFinish.Value)

                    });
                DocumentList.ItemsSource = res.ToList();

            }
            catch (Exception ex)
            {


            }
        }
    }
}
