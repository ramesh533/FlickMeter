using FlickSome.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlickSome.Web.Models
{
    public class MovieModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "ReleaseDate is required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Hero is required")]
        public string Hero { get; set; }

        [Required(ErrorMessage = "Heroin is required")]
        public string Heroin { get; set; }

        public string CharacterArtists { get; set; }

        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Music Director is required")]
        public string MusicDirector { get; set; }

        [Required(ErrorMessage = "Producer is required")]
        public string Producer { get; set; }

        public string Tags { get; set; }

        public List<ReviewModel> Reviews { get; set; }

        public string GetNameByRole(ArtistRole role)
        {
            string name = string.Empty;

            switch (role)
            {
                case ArtistRole.Hero:
                    name = this.Hero;
                    break;
                case ArtistRole.Heroin:
                    name = this.Heroin;
                    break;
                case ArtistRole.Director:
                    name = this.Director;
                    break;
                case ArtistRole.Producer:
                    name = this.Producer;
                    break;
                case ArtistRole.MusicDirector:
                    name = this.MusicDirector;
                    break;
                case ArtistRole.Writer:
                    break;
                case ArtistRole.ScreenPlayWriter:
                    break;
                case ArtistRole.CharacterArtist:
                    break;
                default:
                    break;
            }

            return name;
        }
    }
}