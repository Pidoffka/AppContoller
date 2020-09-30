using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoAppProject.ModelsForMethods
{
    public class AddUserReviewModel
    {
        public string phoneNumberSender { get; set; }
        public string phoneNumberReciever { get; set; }
        public string review { get; set; }
        public bool commend { get; set; }
    }
}
