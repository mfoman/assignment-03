namespace Assignment3.Entities;

public class TagRepository : ITagRepository
{
    private readonly KanbanContext _context;

    public TagRepository(KanbanContext context)
    {
        _context = context;
    }

    public (Response Response, int TagId) Create(TagCreateDTO tag)
    {
        var entity = _context.Tags.FirstOrDefault(c => c.Name == tag.Name);
        Response status;

        if (entity is null)
        {
            entity = new Tag(tag.Name);

            _context.Tags.Add(entity);
            _context.SaveChanges();

            status = Created;
        }
        else
        {
            status = Conflict;
        }

        var created = new TagDTO(entity.TagId, entity.Name);

        return (status, created.Id);
    }

    public Response Delete(int tagId, bool force = false)
    {
        var tag = _context.Tags.FirstOrDefault(c => c.TagId == tagId);
        Response response;

        if (tag is null)
        {
            response = NotFound;
        }
        else if (tag.Tasks.Count != 0 && !force)
        {
            response = Conflict;
        }
        else
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();

            response = Deleted;
        }

        return response;
    }

    public TagDTO? Read(int tagId)
    {
        var tags = from c in _context.Tags
                   where c.TagId == tagId
                   select new TagDTO(c.TagId, c.Name);

        return tags.FirstOrDefault();
    }

    public IReadOnlyCollection<TagDTO> ReadAll()
    {
        var tags = from c in _context.Tags
                   orderby c.Name
                   select new TagDTO(c.TagId, c.Name);

        return tags.ToArray();
    }

    public Response Update(TagUpdateDTO tag)
    {
        var entity = _context.Tags.Find(tag.Id);

        Response response;

        if (entity is null)
        {
            response = NotFound;
        }
        else if (_context.Tags.FirstOrDefault(c => c.TagId != tag.Id && c.Name == tag.Name) != null)
        {
            response = Conflict;
        }
        else
        {
            entity.Name = tag.Name;
            _context.SaveChanges();
            response = Updated;
        }

        return response;
    }
}
