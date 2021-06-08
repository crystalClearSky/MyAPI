using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyAppAPI.Entities;

namespace Entities
{
    public class NoticeEntity
    {
        public int Id { get; set; }
        public string Notice { get; set; }
        public int DecisionByAvatarId { get; set; }
        [ForeignKey("DecisionByAvatarId")]
        public AvatarEntity AvatarEntity { get; set; }
        public int DecisionByGuestId { get; set; }
        [ForeignKey("DecisionByGuestId")]
        public GuestEntity GuestEntity { get; set; }
        public int DisplayCount { get; set; }
        public Decision Choice { get; set; }
        public DateTime NoticeViewedOn { get; set; }
        public enum Decision {
            None,
            Accepted,
            Declined
        }
    }
}