using _0_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using System.Collections.Generic;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;
        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public OperationResult Add(AddComment command)
        {
            var operationResult = new OperationResult();
            var comment = new Comment(command.Name, command.Email, command.Website, command.Message,
                command.OwnerRecordId, command.Type, command.ParentId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operationResult.Succedded();
        }

        public OperationResult Cancel(long id)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operationResult.Faild(ApplicationMessages.RecordNotFount);

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operationResult.Succedded();
        }

        public OperationResult Confirm(long id)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operationResult.Faild(ApplicationMessages.RecordNotFount);

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operationResult.Succedded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
