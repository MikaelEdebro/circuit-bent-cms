using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CircuitBentCMS.Models;
using Resources.Widgets;

namespace CircuitBentCMS.Helpers
{
    public static class Widgets
    {
        public static string InsertWidgets(string input)
        {
            CircuitBentCMSContext context = new CircuitBentCMSContext();
            input = SocialMedia(input, context.SocialMedias.FirstOrDefault());
            input = GetBlogPosts(input, context.Blogs.OrderByDescending(a => a.Date).Take(3));
            input = RecentBlogs(input, context.Blogs.OrderByDescending(a => a.Date).Take(10));
            input = ContactForm(input);
            input = Events(input, context.Shows.Where(a => a.Date > DateTime.Now).OrderBy(a => a.Date));
            input = UpcomingEvents(input, context.Shows.Where(a => a.Date > DateTime.Now).OrderBy(a => a.Date).Take(10));

            input = FacebookFeed(input, context.SocialMedias.FirstOrDefault());
            //input = ImageSlider(input, context.ImageSliderImages.OrderBy(a => a.Order).ToList(), context.ImageSliderSettings.FirstOrDefault());

            return input;
        }

        public static string SocialMedia(string input, SocialMedia socialMedia)
        {
            string insertCode = "<div class='social-media'>";
            string pathToIcons = "/Images/icons/social-media/no-border";
            if (!String.IsNullOrEmpty(socialMedia.Facebook))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Facebook'><img src='{1}/facebook500.png' alt='Facebook' /></a>", socialMedia.Facebook, pathToIcons); 
            }
            if (!String.IsNullOrEmpty(socialMedia.Email))
            {
                insertCode += String.Format("<a href='mailto:{0}' target='_blank' title='E-mail'><img src='{1}/email.png' alt='E-mail' /></a>", socialMedia.Email, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Myspace))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='MySpace'><img src='{1}/myspace.png' alt='MySpace' /></a>", socialMedia.Myspace, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Soundcloud))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='SoundCloud'><img src='{1}/soundcloud.png' alt='SoundCloud' /></a>", socialMedia.Soundcloud, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Twitter))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Twitter'><img src='{1}/twitter.png' alt='Twitter' /></a>", socialMedia.Twitter, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Instagram))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Instagram'><img src='{1}/instagram.png' alt='Instagram' /></a>", socialMedia.Instagram, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.GooglePlus))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Google Plus'><img src='{1}/googleplus.png' alt='Google Plus' /></a>", socialMedia.GooglePlus, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Youtube))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='YouTube'><img src='{1}/youtube.png' alt='YouTube' /></a>", socialMedia.Youtube, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Spotify))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Spotify'><img src='{1}/spotify.png' alt='Spotify' /></a>", socialMedia.Spotify, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Vimeo))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Vimeo'><img src='{1}/vimeo.png' alt='Vimeo' /></a>", socialMedia.Vimeo, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Bandcamp))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='BandCamp'><img src='{1}/bandcamp.png' alt='BandCamp' /></a>", socialMedia.Bandcamp, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Flickr))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Flickr'><img src='{1}/flickr.png' alt='Flickr' /></a>", socialMedia.Flickr, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.LinkedIn))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='LinkedIn'><img src='{1}/linkedin.png' alt='LinkedIn' /></a>", socialMedia.LinkedIn, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.LastFm))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='LastFm'><img src='{1}/lastfm.png' alt='LastFm' /></a>", socialMedia.LastFm, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Pinterest))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Pinterest'><img src='{1}/pinterest.png' alt='Pinterest' /></a>", socialMedia.Pinterest, pathToIcons);
            }
            if (!String.IsNullOrEmpty(socialMedia.Tumblr))
            {
                insertCode += String.Format("<a href='{0}' target='_blank' title='Tumblr'><img src='{1}/tumblr.png' alt='Tumblr' /></a>", socialMedia.Tumblr, pathToIcons);
            }
            
            insertCode += "</div>";

            // insert the code
            input = input.Replace("[widget=socialmedia]", insertCode);

            return input;
        }



        public static string GetBlogPosts(string input, IEnumerable<Blog> blogs)
        {
            string insertCode = "";

            foreach (var item in blogs)
            {
                insertCode += String.Format("<div class='blog-post'><h1>{0} <small>({1}/{2})</small></h1>{3}</div>", 
                    item.Headline, item.Date.Day, item.Date.Month, item.Text);
            }


            // insert the code
            input = input.Replace("[widget=blogposts]", insertCode);

            return input;
        }

        public static string RecentBlogs(string input, IEnumerable<Blog> blogs)
        {
            string insertCode = "<div class='blog-recent'>";

            foreach (var item in blogs)
            {
                insertCode += String.Format("<a href='/blog/{0}'>{1} <span class='blog-date'>({2}/{3})</span></a><br />", 
                    CustomHelpers.CreateSlug(item.Headline), item.Headline, item.Date.Day, item.Date.Month);
            }

            insertCode += "</div>";

            // insert the code
            input = input.Replace("[widget=recentblogs]", insertCode);

            return input;
        }

        public static string ContactForm(string input)
        {
            string insertCode =     String.Format(@"<form id='contact-form' class='form-horizontal'>
                                    <div class='form-group'>
                                        <label for='email' class='control-label col-sm-3'>{0}</label>
                                        <div class='col-sm-9'>
                                            <input type='text' name='email' id='email' class='form-control' />
                                        </div>
                                    </div>
                                    <div class='form-group'>
                                        <label for='subject' class='control-label col-sm-3'>{1}</label>
                                        <div class='col-sm-9'>
                                            <input type='text' name='subject' id='subject' class='form-control' />
                                        </div>
                                    </div>
                                    <div class='form-cheater'>
                                        <input type='text' name='cheater' id='cheater' />
                                        <input type='hidden' name='timestamp' id='timestamp' value='{2}' />
                                    </div>
                                    <div class='form-group'>
                                        <label for='message' class='control-label col-sm-3'>{3}</label>
                                        <div class='col-sm-9'>
                                            <textarea name='message' id='message' class='form-control' rows='10'></textarea>
                                        </div>
                                    </div>
                                    <div class='form-group'>
                                        <div class='col-sm-offset-3 col-sm-9'>
                                            <button type='submit' class='btn btn-primary' id='contact-form-submit'>{4}</button>
                                        </div>
                                    </div>
                                    </form>", 
                                    Resources.Widgets.ContactForm.Email,
                                    Resources.Widgets.ContactForm.Subject,
                                    DateTime.Now,
                                    Resources.Widgets.ContactForm.Message,
                                    Resources.Widgets.ContactForm.SubmitButton
                                    );

            // insert the code
            input = input.Replace("[widget=contactform]", insertCode);

            return input;
        }

        public static string UpcomingEvents(string input, IEnumerable<Show> shows)
        {
            string insertCode = "";

            foreach (var item in shows)
            {
                insertCode += String.Format(@"<div class='event-small clearfix'>
                                            <div class='date'>
                                                <div class='separate'><h5>{0}</h5></div>
                                                <div><h5>{1}</h5></div> 
                                            </div>",
                       item.Date.Day, item.Date.ToString("MMM", System.Globalization.CultureInfo.InvariantCulture));

                // the show is not cancelled
                if (!item.Cancelled)
                {
                    // if the user have supplied a link to the event
                    if (!String.IsNullOrEmpty(item.LinkToEvent))
                    {
                        insertCode += String.Format("<div class='info'><a href='{0}' target='_blank'>{1}</a>, {2}</div>", item.LinkToEvent, item.Title, item.Venue);
                    }
                    else
                    {
                        insertCode += String.Format("<div class='info'>{0}, {1}</div>", item.Title, item.Venue);
                    }
                }
                // the show is cancelled
                else
                {
                    insertCode += String.Format("<div class='info'><span class='show-cancelled'>{0}, {1}<span> {2}</div>", item.Title, item.Venue, Resources.Widgets.Events.Cancelled);
                }

                insertCode += "</div>";
            }

            // insert the code
            input = input.Replace("[widget=upcomingevents]", insertCode);

            return input;
        }

        public static string Events(string input, IEnumerable<Show> shows)
        {
            string insertCode = "";

            foreach (var item in shows)
            {
                insertCode += String.Format(@"<div class='event-big clearfix'>
                                            <div class='date'>
                                                <div class='separate'><h4>{0}</h4></div>
                                                <div><h4>{1}</h4></div> 
                                            </div>
                                            <div class='info'>",
                       item.Date.Day, item.Date.ToString("MMM", System.Globalization.CultureInfo.InvariantCulture));

                // the show is not cancelled
                if (!item.Cancelled)
                {
                    // if the user have supplied a link to the event
                    if (!String.IsNullOrEmpty(item.LinkToEvent))
                    {
                        insertCode += String.Format("<a href='{0}' target='_blank'><b>{1}</b></a>", item.LinkToEvent, item.Title);
                    }
                    else
                    {
                        insertCode += String.Format("<b>{0}</b>", item.Title);
                    }

                    if (!String.IsNullOrEmpty(item.Venue))
                    {
                        insertCode += String.Format("<br /><span>{0}: </span>{1}", Resources.Widgets.Events.Venue, item.Venue);
                    }
            
                    if (!String.IsNullOrEmpty(item.Time))
                    {
                        insertCode += String.Format("<br /><span>{0}: </span>{1}", Resources.Widgets.Events.Time, item.Time);
                    }
            
                    if (!String.IsNullOrEmpty(item.Price))
                    {
                        insertCode += String.Format("<br /><span>{0}: </span>{1}", Resources.Widgets.Events.Price, item.Price);
                    }
                }
                // the show is cancelled
                else
                {
                    insertCode += String.Format("<span class='show-cancelled'>{0}, {1}</span><br><span> {2}</span>", item.Title, item.Venue, Resources.Widgets.Events.Cancelled);
                }

                insertCode += "</div></div>";
            }


            // insert the code
            input = input.Replace("[widget=events]", insertCode);

            return input;
        }


        public static string FacebookFeed(string input, SocialMedia socialMedia)
        {
            string insertCode = String.Format(
                @"<iframe src='//www.facebook.com/plugins/likebox.php?href={0};width=500&amp;height=395&amp;colorscheme=light&amp;show_faces=false&amp;header=false&amp;stream=true&amp;show_border=false'
                scrolling='no' frameborder='0' style='box-shadow: none !important;border: none; overflow: hidden; width: 100%; height: 395px;' 
                allowtransparency='true'></iframe>", socialMedia.Facebook);
            
            // insert the code
            input = input.Replace("[widget=facebookfeed]", insertCode);

            return input;
        }

        public static string ImageSlider(string input, IList<ImageSliderImage> imageSliderImages, ImageSliderSettings imageSliderSettings)
        {
            string insertCode = "";

            // only show the slider if there is any images
            if (imageSliderImages.Count > 0)
            {
                insertCode += "<ul class='bxslider'>";

                // loop through the images
                foreach (var image in imageSliderImages)
                {
                    // add the images
                    insertCode += String.Format(@"<li><a href='{0}' title='{1}'>
                        <img src='/Images/ImageSlider/{2}' alt='{2}' /></a></li>", image.LinkUrl, image.Title, image.ImageUrl);
                }

                // insert .js and css files and initialize the script
                insertCode += String.Format(
                @"</ul><script>
                    $(document).ready(function(){{
                        $('.bxslider').bxSlider({{
                            auto: true,
                            pause: {0},
                            speed: 1000,
                            controls: false,
                            autoHover: true
                        }});
                    }});
                </script>", imageSliderSettings.TransitionSpeed * 1000);
            }

            // insert the code
            input = input.Replace("[widget=imageslider]", insertCode);

            return input;
        }
    }
}