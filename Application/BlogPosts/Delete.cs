using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Site.Persistence;

namespace Site.Application.BlogPosts
{
    public class Delete
    {
        public class Command: IRequest
        {
            public Guid Id { get; set; }
        }
    
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
    
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var blogpost = await _context.BlogPosts.FindAsync(request.Id);

                _context.Remove(blogpost);
    
                var success = await _context.SaveChangesAsync() > 0;
    
                if(success) return Unit.Value;
    
                throw new Exception("Problem saving changes");
            }
        }
    }
}