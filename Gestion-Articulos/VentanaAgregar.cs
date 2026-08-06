using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;
using System.Configuration;

namespace Gestion_Articulos
{
    public partial class Ventana_agregar : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        public Ventana_agregar()
        {
            InitializeComponent();
        }
        public Ventana_agregar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
            
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            
            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.codigoArticulo = txtCodigo.Text.ToUpper();
                articulo.nombre = txtNombre.Text;
                articulo.descripcion = txtDescripcion.Text;
                articulo.urlImagen = txtImagenUrl.Text;
                articulo.marcas =(Marca)cboMarca.SelectedItem;
                articulo.categorias = (Categoria)cboCategoria.SelectedItem;
                articulo.precio = decimal.Parse(txtPrecio.Text);

                if(articulo.id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente!!");
                }
                else
                {
                    negocio.agregarProducto(articulo);
                    MessageBox.Show("Se agrego exitosamente!!!");
                }
                if(archivo != null  && !(txtImagenUrl.Text.ToUpper().Contains("HTTP")))
                {
                    File.Copy(archivo.FileName,ConfigurationManager.AppSettings["ImagenArticulo"] + archivo.SafeFileName);
                }
                
                Close();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private void Ventana_agregar_Load(object sender, EventArgs e)
        {
             CategoriaNegocio negocio2 = new CategoriaNegocio();
             MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                cboMarca.DataSource = negocio.list();
                cboMarca.ValueMember = "id";
                cboMarca.DisplayMember = "descripcion";
            
                cboCategoria.DataSource = negocio2.list();
                cboCategoria.ValueMember = "id";
                cboCategoria.DisplayMember = "descripcion";

                if (articulo != null)
                {
                    lblTitulo.Text = "Modifiar Articulo";         
                    txtCodigo.Text = articulo.codigoArticulo;
                    txtNombre.Text =articulo.nombre;
                    txtDescripcion.Text =articulo.descripcion;
                    txtImagenUrl.Text = articulo.urlImagen;
                    cargarImagen(articulo.urlImagen);
                    cboMarca.SelectedValue = articulo.marcas.id;
                    cboCategoria.SelectedValue = articulo.categorias.id;
                    txtPrecio.Text = articulo.precio.ToString("N2");

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

            
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagenModificar.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxImagenModificar.Load("https://editorial.unc.edu.ar/wp-content/uploads/sites/33/2022/09/placeholder.png");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            //creamos el objeto para agregar un archivo
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
                
            }
        }
    }
}
