using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using System.Collections.Generic;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public OperationResult Create(CreateRole command)
        {
            var operationResult = new OperationResult();
            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operationResult.Faild(ApplicationMessages.DuplicatedRecord);

            var role = new Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operationResult.Succedded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operationResult = new OperationResult();
            var role = _roleRepository.Get(command.Id);

            if (role == null)
                return operationResult.Faild(ApplicationMessages.RecordNotFount);

            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operationResult.Faild(ApplicationMessages.DuplicatedRecord);

            role.Edit(command.Name);
            _roleRepository.SaveChanges();
            return operationResult.Succedded();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> List()
        {
            return _roleRepository.List();
        }
    }
}
