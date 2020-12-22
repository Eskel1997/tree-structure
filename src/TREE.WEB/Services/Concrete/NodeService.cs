using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TREE.DB.Entities;
using TREE.DB.Repositories.Abstract;
using TREE.WEB.Services.Abstract;
using TREE.WEB.ViewModels;

namespace TREE.WEB.Services.Concrete
{
    internal class NodeService : INodeService
    {
        private static string MESSAGE_NOT_FOUND = "The node has not been found.";
        private static string MESSAGE_NODE_DELETED = "The node has been deleted.";
        private static string MESSAGE_NODE_NOT_DELETED = "The node has not been deleted.";
        private static string MESSAGE_NODE_UPDATED = "The node has been modified.";
        private static string MESSAGE_NODE_NOT_UPDATED = "The node has not been modified.";
        private static string MESSAGE_NODE_ADD = "The node has been add.";
        private static string MESSAGE_NODE_NOT_ADD = "The node has not been added.";

        private readonly INodeRepository nodeRepository;
        private readonly IMapper mapper;

        public NodeService(INodeRepository nodeRepository,
            IMapper mapper)
        {
            this.nodeRepository = nodeRepository;
            this.mapper = mapper;
        }
        public async Task<NodeViewModel> GetByIdAsync(long id)
        {
            var node = await this.nodeRepository.GetByIdAsync(id);

            var response = this.mapper.Map<NodeViewModel>(node);
            response.IsSuccess = true;
            return response;
        }

        public async Task<List<LabelValueViewModel>> GetSuggestions()
        {
            var suggestions = await this.nodeRepository.GetSuggestions();

            return this.mapper.Map<List<LabelValueViewModel>>(suggestions);
        }

        public async Task<List<NodeViewModel>> GetAllAsync()
        {
            var nodes = await this.nodeRepository.GetAllAsync();

            return this.mapper.Map<List<NodeViewModel>>(nodes);
        }

        public async Task<NodeViewModel> UpdateAsync(NodeEditViewModel request)
        {
            var node = await this.nodeRepository.GetByIdAsync(request.Id);

            if (node == null)
            {
                return new NodeViewModel
                {
                    IsSuccess = false,
                    Message = MESSAGE_NOT_FOUND
                };
            }

            node.Update(request.Name, request.ParentId);

            try
            {
                await this.nodeRepository.UpdateAsync(node);
                await this.nodeRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new NodeViewModel
                {
                    IsSuccess = false,
                    Message = MESSAGE_NODE_NOT_UPDATED
                };
            }

            return new NodeViewModel
            {
                IsSuccess = true,
                Message = MESSAGE_NODE_UPDATED
            };
        }

        public async Task<NodeViewModel> RemoveAsync(long id)
        {
            var node = await this.nodeRepository.GetByIdAsync(id);

            if (node == null)
            {
                return new NodeViewModel
                {
                    IsSuccess = false,
                    Message = MESSAGE_NOT_FOUND
                };
            }

            try
            {
                await this.nodeRepository.RemoveAsync(node);
                await this.nodeRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new NodeViewModel
                {
                    IsSuccess = false,
                    Message = MESSAGE_NODE_NOT_DELETED
                };
            }

            return new NodeViewModel
            {
                IsSuccess = true,
                Message = MESSAGE_NODE_DELETED
            };
        }

        public async Task<NodeViewModel> AddAsync(NodeAddViewModel request)
        {
            var node = new Node(request.Name, request.ParentId);

            try
            {
                await this.nodeRepository.AddAsync(node);
                await this.nodeRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new NodeViewModel
                {
                    IsSuccess = false,
                    Message = MESSAGE_NODE_NOT_ADD
                };
            }

            return new NodeViewModel
            {
                IsSuccess = true,
                Message = MESSAGE_NODE_ADD
            };
        }
    }
}
