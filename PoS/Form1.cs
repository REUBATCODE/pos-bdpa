using MySql.Data.MySqlClient;
namespace PoS
{
    public partial class Form1 : Form
    {
        //Declaracion de variables
        MySqlConnection con = new MySqlConnection("server=localhost; database=pos; uid=root; pwd=;");
        MySqlCommand comando;
        MySqlDataReader leer;
        String comandoSelect = "";
        String cantidad = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ESTABLECEMOS EL LEABLE DANDOLE LA HORA Y FECHA. 
            HoraFecha.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            //Localizacion del lable, con la linea 13, hacemos que se localice en la mitad.
            titulo.Location = new Point((this.Width / 2) - (titulo.Width / 2), 0);//ESTE LO LOCALIZAMOS EN EL CENTRO
            //Centramos el desarrollado por, sobre el titulo que realizamos la inicio. 
            Desarrollado.Location = new Point((this.Width / 2) - (Desarrollado.Width / 2), titulo.Height);//SE LOCALIZA EN EL CENTRO PERO LE DEBEMOS DE SUMAR LA ALTURA YA ESTABLECIDA DEL TITULO INICIAL
            //Hora y fecha está a altura sumada de lo que vale titulo, desarrollado, centrado. 
            HoraFecha.Location = new Point((this.Width / 2) - (HoraFecha.Width / 2), (titulo.Height + Desarrollado.Height));//SE ESTABLECE EN EL CENTOR DEBAJO DEL TITULO ANTERIOR
            //Localizamos el datagrid, según al nivel de hora y fecha
            dataGridProductos.Location = new Point((10), (titulo.Height + Desarrollado.Height + HoraFecha.Height));//CENTRAMOS EL DATAGRID SEPARANDOLO 10 PIXELES DE CADA LADO
            dataGridProductos.Width = this.Width - 20;//Con esto separamos 10 pixeles entre el objeto y la ventana, de cada lado. 
            dataGridProductos.Height = (this.Height / 4) * 3;//Con esto le damos al 3/4 segun la altura DE TODOS LOS PIXELES
            pictureBoxLogo.Location = new Point((this.Width - pictureBoxLogo.Width), 0); //ESTABLECEMOS QUE ESTÉ EN LA ESQUINA DE LA PANTALLA
            txtCodigo.Location = new Point((10), this.Height - txtCodigo.Height);
            txtCodigo.Width = this.Width - 20;//esta segun el nivel del data grid. 

            //AÑADIMOS COLUMNAS, NOMBRES, EDITAMOS EL ESTILO Y DETERMINAMOS EL TAMAÑO
            //INICIAMOS ESTABLECIENDO LAS PROPIEDADES DE CANTIDAD. 
            dataGridProductos.Columns.Add("Cantidad", "Cantidad");//Aqui añadimos las columnas. 
            dataGridProductos.Columns[0].HeaderCell.Style.Font = new Font("Arial", 25F);//Esta es la propiedad para cambiarle el tipo de letra tamaño
            dataGridProductos.Columns[0].Width = (dataGridProductos.Width * 10) / 100;//Damos el tamaño de cada columna
            //SE ESTABLECE LAS PROPIEDADES DE LA COLUMNA NOMBRE
            dataGridProductos.Columns.Add("Nombre", "Nombre");//Aqui añadimos las columnas. 
            dataGridProductos.Columns[1].Width = ((dataGridProductos.Width) * 50) / 100;//Damos el tamaño de la columna 2
            dataGridProductos.Columns[1].HeaderCell.Style.Font = new Font("Arial", 25F);
            //SE ESTABLECE LAS PRROPIEDADES DE LA COLUMNA PRECIO
            dataGridProductos.Columns.Add("Precio", "Precio");//Aqui añadimos las columnas. 
            dataGridProductos.Columns[2].Width = ((dataGridProductos.Width) * 20) / 100;//tamaño de la columna 3
            dataGridProductos.Columns[2].HeaderCell.Style.Font = new Font("Arial", 25F);
            //SE ESTABLECE LAS PROPIEDADES DE LA COLUMNA TOTAL
            dataGridProductos.Columns.Add("Total", "Total");//Aqui añadimos las columnas. 
            dataGridProductos.Columns[3].Width = ((dataGridProductos.Width) * 20) / 100;//tamaño de la columna 4 
            dataGridProductos.Columns[3].HeaderCell.Style.Font = new Font("Arial", 25F);
            //IMPEDIR LA EDICIÓN DEL DATAGRID
            //LA PROPIEDAD ReadOnly, PERMITE QUE SE PUEDA O NO MODIFICAR EL TEXTO. 
            dataGridProductos.ReadOnly = true;
            //MODIFICAMOS LAS PROPIDADES DEL LABLE TOTAL. 
            Font fuente = new Font("Arial", 25);//AQUI ESTAMOS MODIFICANDO EL TAMAÑO DE LA FUENTE Y EL TIPO. 
            lableTotal.Font = fuente;//EL RESULTADO LO MANDAMOS AL VALOR DEL LABLE. 
            //CAMBIAMOS LA POSICIÓN DEL TXT TOTAL SUMANDO LAS POSICIONES DE ALGUNOS DE LOS COMPONENTES DE ESTE. 
            int posicion = (this.Height - (titulo.Height + Desarrollado.Height + HoraFecha.Height + dataGridProductos.Height + txtCodigo.Height));
            lableTotal.Location = new Point(10 + dataGridProductos.Columns[0].Width +
                dataGridProductos.Columns[1].Width + dataGridProductos.Columns[2].Width, (titulo.Height +
                Desarrollado.Height + HoraFecha.Height + dataGridProductos.Height) + posicion / 4);
            //HACEMOS QUE EL MOUSE SIEMPRE APAREZCA EN EL TXT DEL CODIGO
            txtCodigo.TabIndex = 0;
            //QUITAMOS LAS LINEAS DE LA TABLA
            this.dataGridProductos.CellBorderStyle = DataGridViewCellBorderStyle.None;

        }
        //Personalizamos el lable de hora y fecha según la hora y fecha del sistema, esta se actualiza. 
        private void time_Tick(object sender, EventArgs e)
        {
            //AL LEABLE 
            HoraFecha.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
        }
        //CON ESTE EVENTO, NO DEJAMOS MOVER AL USUARIO CON EL CODE. 
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }
        //PARA QUE FUNCIONE LA TECLA ENTER. 
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)//PARA CUANDO EL USUARIO LE DE ENTER SE MANDE EL MENSAJE
            {
                try
                {
                    if (txtCodigo.Text.IndexOf('*') != 1)
                    {
                        String[] producto = txtCodigo.Text.Split('*');
                        cantidad = producto[0];
                        MessageBox.Show("Viene cantidad" + producto[0] + producto[1]);
                        comandoSelect = "SELECT * FROM productos WHERE codigo = " + "'" + producto[1] + "'";
                    }
                    else
                    {
                        MessageBox.Show("No viene cantidad");
                        cantidad = "1";
                        comandoSelect = "SELECT * FROM productos WHERE codigo = " + "'" + txtCodigo.Text + "'";
                    }
                    //MessageBox.Show("SELECT * FROM productos WHERE codigo = 
                    con.Open();
                    comando = new MySqlCommand(comandoSelect, con);
                    leer = comando.ExecuteReader();
                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            dataGridProductos.Rows.Add(cantidad.ToString(), leer.GetString(1), leer.GetString(2), (double.Parse(leer.GetString(2)) * double.Parse(cantidad.ToString())));
                            //Ciclo FOREACH para obtener los datos del total
                            double sumaTotal = 0;
                            foreach (DataGridViewRow row in dataGridProductos.Rows)
                            {
                                DataGridViewCell cell = row.Cells[3];
                                if (cell.Value != null)
                                {
                                    double total = double.Parse(cell.Value.ToString());
                                    sumaTotal += total;
                                }
                            }
                            double numRedondeado = Math.Round(sumaTotal, 2);
                            lableTotal.Text = "Total $ " + Math.Round(sumaTotal, 2).ToString();
                        }
                    }
                    con.Close();
                }
                catch (Exception)
                {

                    throw;
                }
                MessageBox.Show("SELECT * FROM productos WHERE codigo = " + "'" + txtCodigo.Text + "'");
                txtCodigo.Clear();
            }
        }
    }
}