using System;
using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Owner Owner { get; set; }
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }
    }

    public class OwnerFormViewModel
    {
        public Owner Owner { get; set; }
        public List<Neighborhood> Neighborhoods { get; set; }
    }

    public class WalkerDetailView
    {
        public Walker Walker { get; set; }
        public List<Walks> Walks { get; set; }
    }
}