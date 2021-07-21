using BookMgtSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BookMgtSystem.Controllers
{
    public class AuthorApiController : ApiController
    {
        private SystemEntities db = new SystemEntities();
        //
        // GET: /AuthorApi/

        public AuthorsViewModel Get(int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var results = from b in db.Books
                          select new
                          {
                              b.BookId,
                              b.Title,
                              Checked = ((from ab in db.AuthorBook
                                          where (ab.AuthorId == id) && (ab.BookId == b.BookId)
                                          select ab).Count() > 0)
                          };
            var myViewModel = new AuthorsViewModel();
            myViewModel.AuthorId = id;
            myViewModel.FirstName = author.FirstName;
            myViewModel.LastName = author.LastName;
            myViewModel.DateOfBirth = author.DateOfBirth;

            var myCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in results)
            {
                if(item.Checked == true)
                myCheckBoxList.Add(new CheckBoxViewModel { Id = item.BookId, Name = item.Title, Checked = item.Checked });
            }
            myViewModel.Books = myCheckBoxList;
            return myViewModel;
        }

    }
}
