﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ReadMagazine.Domain.Helpers;

namespace ReadMagazine.Domain.Entities
{
    public class Noticia
    {

        [HiddenInput(DisplayValue = false)]
        public int Id_Noticia { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Please enter a Channel")]
        public int Id_Channel { get; set; }

        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a Link")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Please enter a Descripcion")]
        public string Descripcion { get; set; }
        public string DescripcionSoloTexto
        {
            get
            {
                return HtmlRemoval.StripTagsCharArray(this.Descripcion);
            }
        }

        public string WidthClass { get; set; }
        public string HeigthClass { get; set; }
        public string Contenido { get; set; }
        public string ContenidoSinImagenes
        {
            get
            {
                return ExtractImages.RemoveTagsImages(this.Contenido, 1);
            }
        }
        public string ContenidoSoloTexto
        {
            get
            {
                return ExtractImages.RemoveTagsImages(this.Contenido, null);
            }
        }


        public int HeigthMaxImages
        {
            get
            {
                string urlTapa;
                var heigth = ExtractImages.ReadImagesWithMaxHeigth(this.Descripcion + this.Contenido, out urlTapa);
                this.UrlImageTapa = urlTapa;
                return heigth;
            }

        }
        public string UrlImage { get; set; }
        private string _urlImageTapa;
        public string UrlImageTapa
        {
            get
            {
                var url = ExtractImages.ExtractFirstHtmlImage(this.Descripcion + this.Contenido);
                return url;
            }
            set
            {
                _urlImageTapa = value;
            }

        }
    }
}
