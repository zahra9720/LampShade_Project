using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;
        public ArticleCategoryRepository(BlogContext blogContext) : base(blogContext)
        {
            _context = blogContext;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
        }

        public string GetSlugBy(long id)
        {
            return _context.ArticleCategories.Select(x => new { x.Id, x.Slug })
                .FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ShowOrder = x.ShowOrder,
                    Picture = x.Picture,
                    ArticleCount = x.Articles.Count,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x => x.ShowOrder).ToList();
        }
    }
}
