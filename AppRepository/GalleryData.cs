using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;
using System.Collections.Generic;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;
using MyAppAPI.Models;

namespace MyAppAPI.AppRepository
{
    public class GalleryData : IGalleryData, IVote
    {
        public static GalleryData Current { get; } = new GalleryData();
        public List<GalleryCard> Cards { get; set; }
        public GalleryData()
        {
            Cards = new List<GalleryCard>()
            {
                new GalleryCard()
                {
                    Id = 1,
                    Title = "Revenge of Koria",
                    CreatedOn = new DateTime(2020,8,23),
                    ImageUrl = "",
                    Flag = false,
                    Descritpion = "This was a personal project I worked on for four months. The idea behind the creation of this creator started after I what a Japanese anime film called Princess Mononoke.",
                    Tags =
                    {
                        new Tag()
                        {
                            // Id = 1,
                            TagItem = "3dWorks",
                        },
                        new Tag()
                        {
                            // Id = 2,
                            TagItem = "drawings",
                        },
                        new Tag()
                        {
                            // Id = 5,
                            TagItem = "lady",
                        }
                    },
                    UpVotes = new List<Vote>()
                    {
                        new Vote()
                        {
                            Id = 1,
                            UpVote = true,
                            VoteById = 2,
                        },

                        new Vote()
                        {
                            Id = 3,
                            UpVote = true,
                            VoteById = 1
                        },
                        new Vote()
                        {
                            Id = 7,
                            UpVote = true,
                            VoteById = 3
                        }
                    }
                    
                    
                    
                    // Tag with #3dworks, #zbrush
                    
                },
                new GalleryCard()
                {
                    Id = 2,
                    Title = "Jungle Woman",
                    CreatedOn = new DateTime(2020,5,18),
                    ImageUrl = "",
                    Flag = false,
                    Tags =
                    {
                        new Tag()
                        {
                            // Id = 1,
                            TagItem = "3dWorks",
                        },
                        new Tag()
                        {
                            // Id = 5,
                            TagItem = "zbrush",
                        },
                        new Tag()
                        {
                            // Id = 5,
                            TagItem = "gold",
                        },
                        new Tag()
                        {
                            // Id = 5,
                            TagItem = "crystal",
                        }
                    },
                    UpVotes = new List<Vote>()
                    {
                        new Vote()
                        {
                            Id = 2,
                            UpVote = true,
                            VoteById = 1
                        },
                        new Vote()
                        {
                            Id = 4,
                            UpVote = true,
                            VoteById = 2
                        },


                    }

                },
                new GalleryCard()
                {
                    Id = 3,
                    Title = "A Different Artwork",
                    CreatedOn = new DateTime(2020,5,18),
                    ImageUrl = "",
                    Flag = false,
                    Tags =
                    {
                        new Tag()
                        {
                            // Id = 1,
                            TagItem = "drawings",
                        },
                        new Tag()
                        {
                            // Id = 4,
                            TagItem = "artworks",
                        },
                        new Tag()
                        {
                            // Id = 5,
                            TagItem = "zbrush",
                        }
                    },
                    UpVotes = new List<Vote>()
                    {

                        new Vote()
                        {
                            Id = 5,
                            UpVote = true,
                            VoteById = 3
                        }

                    }
                }
            };
        }

        public IEnumerable<GalleryCard> getAllCards()
        {
            return Cards.OrderBy(x => x.Id);
        }

        public GalleryCard getCardById(int id)
        {
            return Cards.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GalleryCard> GetGalleryCardsByTags(List<Tag> tags)
        {
            List<GalleryCard> galleryCards = new List<GalleryCard>();
            string word = string.Empty;

            // for (int i = 0; i < tags.Count(); i++)
            // {
            //     word = (string)tags[i].TagItem;
            //     foreach (var card in Cards)
            //     {
            //         if (card.Tags.Any(x => (string)x.TagItem == (string)tags[i].TagItem))
            //         {
            //             galleryCards.Add(card);
            //         }
            //     }
            // }

            // foreach (var card in Cards)
            // {
            //     for (int i = 0; i < tags.Count; i++)
            //     {
            //         if (card.Tags.Any(x => (string)x.TagItem == (string)tags[i].TagItem))
            //         {
            //             galleryCards.Add(card);
            //         }
            //     }
            // }
            int count = tags.Count - 1;
            int innerCount = tags.Count;
            // var result = new List<GalleryCard>();
            var reCards = Cards;
            string word2 = string.Empty;
            do
            {
                var result = new List<GalleryCard>();
                foreach (var card in reCards)
                {
                    // Fix to lower
                    // Fix empty string ""
                    if (card.Tags.Any(x => ((string)x.TagItem).ToLower() == ((string)tags[count].TagItem).ToLower()))
                    {
                        result.Add(card);
                    }
                }

                if (innerCount > 0)
                {
                    reCards = result;
                    count--;
                    innerCount--;
                }

            } while (innerCount > 0);
            galleryCards = reCards;
            return galleryCards;
            // http://localhost:5000/api/content/tag?words=3dworks&words=artworks

        }

        public void AddGalleryCard(GalleryCard galleryCard)
        {
            GalleryData.Current.Cards.Add(galleryCard);
        }

        public void DeleteGalleryCardById(int id)
        {
            var cardToDelete = GalleryData.Current.getCardById(id);
            if (cardToDelete != null)
            {
                GalleryData.Current.Cards.Remove(cardToDelete);
            }
        }

        public bool AddVote(Avatar avatar, int id)
        {
            bool HasVoted = false;
            bool votable = true;
            var galleryCard = GalleryData.Current.getCardById(id);
            var maxVoteId = GalleryData.Current.GetAllVotes().Max(v => v.Id) + 1;

            foreach (var vote in galleryCard.UpVotes)
            {
                if (vote.VoteById == avatar.Id)
                {
                    votable = false;
                }
            }
            if (votable)
            {
                var newVote = new Vote()
                {
                    Id = maxVoteId,
                    UpVote = true,
                    VoteById = avatar.Id
                };
                galleryCard.UpVotes.Add(newVote);
                HasVoted = true;
            }

            return HasVoted;
        }

        public IEnumerable<Vote> GetVotesByAvatar(int id)
        {
            var result =
            from gallery in GalleryData.Current.Cards
            from votes in gallery.UpVotes
            where votes.VoteById == id
            select votes;

            return result;
        }

        public void RemoveVote(Avatar avatar, int id)
        {
            var toRemove =
            from gallery in GalleryData.Current.Cards
            from votes in gallery.UpVotes
            where votes.VoteById == avatar.Id
            select votes;
            var gCard = GalleryData.Current.getCardById(1);
            gCard.UpVotes.RemoveAll(v =>v.VoteById == avatar.Id);
            
        }

        
        public List<Vote> GetAllVotes()
        {
            var upVotes = GalleryData.Current.Cards.Select(g => g.UpVotes);
            var allVotes = new List<Vote>();
            foreach (var vote in upVotes)
            {
                foreach (var item in vote)
                {
                    allVotes.Add(item);
                }
            }

            return allVotes;
        }

    }
}