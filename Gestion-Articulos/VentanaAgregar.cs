using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace Gestion_Articulos
{
    public partial class Ventana_agregar : Form
    {
        public Ventana_agregar()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevos = new Articulo();
            try
            {
                nuevos.codigoArticulo = txtCodigo.Text;
                nuevos.nombre = txtNombre.Text;
                nuevos.descripcion = txtDescripcion.Text;
                nuevos.urlImagen = txtImagenUrl.Text;
                nuevos.marcas =(Marca)cboMarca.SelectedItem;
                nuevos.categorias = (Categoria)cboCategoria.SelectedItem;
                nuevos.precio = decimal.Parse(txtPrecio.Text);
                negocio.agregarProducto(nuevos);
                MessageBox.Show("Se agrego exitosamente!!!");
                Close();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void Ventana_agregar_Load(object sender, EventArgs e)
        {
            
            MarcaNegocio negocio = new MarcaNegocio();
            cboMarca.DataSource = negocio.list();
            CategoriaNegocio negocio2 = new CategoriaNegocio();
            cboCategoria.DataSource = negocio2.list();

            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
