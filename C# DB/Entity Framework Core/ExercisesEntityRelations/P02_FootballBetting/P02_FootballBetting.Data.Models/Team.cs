﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        [MaxLength(3)]
        public string Initials { get; set; }

        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }

        [ForeignKey(nameof(PrimaryKitColorId))]
        public virtual Color PrimaryKitColor { get; set;}

        public int SecondaryKitColorId { get; set; }

        [ForeignKey(nameof(SecondaryKitColorId))]
        public virtual Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        [InverseProperty(nameof(Game.HomeTeam))]
        public virtual ICollection<Game> HomeGames { get; set; }

        [InverseProperty(nameof(Game.AwayTeam))]
        public virtual ICollection<Game> AwayGames { get; set; }
    }
}
