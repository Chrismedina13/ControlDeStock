using ControlDeStock.Context;
using ControlDeStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ControlDeStock.Utils;

namespace ControlDeStock.Managers
{
    public class UbicacionManager
    {
        private AppDBContext context;

        public UbicacionManager(AppDBContext context)
        {
            this.context = context;
        }

        public string Validar(Ubicacion ubicacion)
        {
            string errores = null;
            DepositoManager manDeposito = new DepositoManager(context);
            if (manDeposito.ValidarExistencia(ubicacion.DepositoID))
                errores += "Deposito no valido\n";
            if (!this.FormatoCodUbicacionValido(ubicacion.CodUbicacion))
                errores += errores + "Codigo de ubicación invalido";

            return errores;                                                    
        }

        public bool FormatoCodUbicacionValido(string codUbicacion)
        {
            bool valido = true;
            var partesPatron = codUbicacion.Split("-").ToList();
            if (partesPatron.Count != 4)
                valido = false;
            
            if (valido) {
               
                foreach (string parte in partesPatron)
                {
                    if (parte.Length != 2)
                        valido = false;
                }
            }

            return valido;
        }

        public bool ValidarExistencia(string depositoID,string codUbicacion) {

            return context.Ubicacion.Any(x => x.DepositoID == depositoID && x.CodUbicacion == codUbicacion);
        }

        public async Task<bool> ProductoAlmacenadoEnDepositosAsync(string codArticulo) {

            bool productoAlmacenado = false;
            try
            {
                HttpClient cliente = new HttpClient();
                cliente.BaseAddress = new Uri(Constantes.ApiMercadoLibreURLBase);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string URLApi = Constantes.ApiMercadoLibreURLGETProductos + codArticulo;

                HttpResponseMessage respuesta = await cliente.GetAsync(URLApi);
                if (respuesta.IsSuccessStatusCode) { 
                    var read = await respuesta.Content.ReadAsStringAsync();
                    JObject resultadoJson = JObject.Parse(read);
                    var shipping = resultadoJson.SelectToken("shipping");
                    var logistic_type = shipping.SelectToken("logistic_type");
                    if ((string)logistic_type == Constantes.Almacenamiento)
                        productoAlmacenado = true;                
                }
                   
            }
            catch (Exception ex)
            {

                throw;
            }

            return productoAlmacenado;
        }

    }
}
