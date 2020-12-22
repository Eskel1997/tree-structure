using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TREE.WEB.Services.Abstract;
using TREE.WEB.ViewModels;

namespace TREE.WEB.Controllers
{
    public class TreeController : Controller
    {
        private readonly INodeService nodeService;
        private readonly IMapper mapper;

        public TreeController(
            INodeService nodeService,
            IMapper mapper)
        {
            this.nodeService = nodeService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<NodeViewModel>> GetAll()
        {
            return await this.nodeService.GetAllAsync();
        }

        [HttpGet]
        public async Task<List<LabelValueViewModel>> GetSuggestions()
        {
            return await this.nodeService.GetSuggestions();
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await this.nodeService.RemoveAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NodeAddViewModel request)
        {
            if (ModelState.IsValid)
            {
                var result = await this.nodeService.AddAsync(request);

                if (!result.IsSuccess)
                {
                    return BadRequest();
                }

                return Ok(result.Message);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var result = await this.nodeService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            var model = this.mapper.Map<NodeEditViewModel>(result);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] NodeEditViewModel request)
        {
            if (ModelState.IsValid)
            {
                var result = await this.nodeService.UpdateAsync(request);

                if (!result.IsSuccess)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result.Message);
            }

            return BadRequest();
        }
    }
}
