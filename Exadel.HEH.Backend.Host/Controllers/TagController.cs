using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class TagController
    //{
    //    private readonly TagRepository _tagRepository;

    //    public TagController(TagRepository tagRepository)
    //    {
    //        _tagRepository = tagRepository;
    //    }

    //    [HttpGet]
    //    public Task<IEnumerable<Tag>> Get()
    //    {
    //        return _tagRepository.Get();
    //    }

    //    [HttpGet("{id}")]
    //    public Task<Tag> Get(Guid id)
    //    {
    //        var tag = _tagRepository.GetByIdAsync(id);

    //        return tag == null ? null : _tagRepository.GetByIdAsync(id);
    //    }

    //    [HttpPost]
    //    public Task Post(Tag tag)
    //    {
    //        return _tagRepository.CreateAsync(tag);
    //    }

    //    [HttpPut("{id}")]
    //    public Task<Tag> Put(Guid id, Tag tagInsert)
    //    {
    //        var tag = _tagRepository.GetByIdAsync(id);

    //        if (tag == null)
    //        {
    //            return null;
    //        }

    //        _tagRepository.UpdateAsync(id, tagInsert);

    //        return _tagRepository.GetByIdAsync(id);
    //    }

    //    [HttpDelete("{id}")]
    //    public string Delete(Guid id)
    //    {
    //        var tag = _tagRepository.GetByIdAsync(id);

    //        if (tag == null)
    //        {
    //            return null;
    //        }

    //        _tagRepository.RemoveAsync(id);

    //        return "Deleted";
    //    }
    //}
}