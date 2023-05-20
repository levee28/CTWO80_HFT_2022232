﻿using CTWO80_HFT_2022232.Models;
using CTWO80_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTWO80_HFT_2022232.Logic
{
    internal class PlayerLogic 
    {
        IRepository<Player> repo;

        public PlayerLogic(IRepository<Player> repo)
        {
            this.repo = repo;
        }

        public void Create(Player item)
        {
            if (item.PlayerPosition!="striker"|| item.PlayerPosition != "defender"|| item.PlayerPosition != "goalkeeper"|| item.PlayerPosition != "midfielder")
            {
                throw new ArgumentException("The position does not exists.");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Player Read(int id)
        {
           
            
            return this.repo.Read(id);
        }

        public IQueryable<Player> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Player item)
        {
            this.repo.Update(item);
        }
        //egy player name alapján hány trófeát nyert és milyen poszton játszik
        public IEnumerable<KeyValuePair<string, int>> PlayerTeamAndPosition(string name)
        {
            return from x in this.repo.ReadAll()
                   where x.PlayerName == name
                   select new KeyValuePair<string, int>(x.PlayerPosition, x.FootballTeam.TrophiesWon);
        }

        //poziciónként a legtöbb nyert trófea
        public IEnumerable<KeyValuePair<string,int>> ThrophiesByPosition()
        {
            return from x in this.repo.ReadAll()
                   group x by x.PlayerPosition into g
                   select new KeyValuePair<string, int>(g.Key, g.Sum(t => t.FootballTeam.TrophiesWon));
        }
    }

   
}
