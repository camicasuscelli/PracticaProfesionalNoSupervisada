using System.Xml.Serialization;

namespace MLM.Common.DTO
{
    [System.Serializable]
    public class Usuario
    {
        private string _nombre;
        private string _apellido;
        private int _dni;

        //esto es para que re arme el objeto.
        [XmlElement(ElementName = "NombreXml")]
        public string Nombre { get => _nombre; set => _nombre = value; }

        
        [XmlElement(ElementName = "ApellidoXml")]
        public string Apellido { get => _apellido; set => _apellido = value; }

        
        [XmlElement(ElementName = "DniXml")]
        public int Dni { get => _dni; set => _dni = value; }
    }
}
