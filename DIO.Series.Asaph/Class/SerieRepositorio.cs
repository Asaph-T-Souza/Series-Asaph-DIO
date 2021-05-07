using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIO.Series.Asaph
{
	public class SerieRepositorio : IRepositorio<Serie>
	{
		private List<Serie> listaSerie = new List<Serie>();
		public void Atualiza(int id, Serie objeto)
		{
			listaSerie[id] = objeto;
		}

		public void Exclui(int id)
		{
            try
            {
				listaSerie[id].Excluir();
			}
            catch
            {
				Console.WriteLine("Id Inexisente");
            }
		}

		public void Insere(Serie objeto)
		{
			listaSerie.Add(objeto);
		}

		public List<Serie> Lista()
		{
			return listaSerie;
		}

		public int ProximoId()
		{
			return listaSerie.Count;
		}

		public Serie RetornaPorId(int id)
		{
            try
            {           
			return listaSerie[id];
			}
			catch(ArgumentOutOfRangeException)
            {
				Console.WriteLine("Id Inexistente");
				return null;
            }
		}
	}
}
