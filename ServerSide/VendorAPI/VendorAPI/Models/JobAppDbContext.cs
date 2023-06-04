using Microsoft.EntityFrameworkCore;

namespace ConsumerAPI.Models
{
    public class JobAppDbContext : DbContext
    {
        public JobAppDbContext(DbContextOptions<JobAppDbContext> options) : base(options) { }
        public DbSet<Consumer> AllConsumers { get; set; }
        public DbSet<Vendor> AllVendors { get; set; }
        public DbSet<Candidate> AllCandidates { get; set; }
        public DbSet<JobDetails> AllJobs { get; set; }
        public DbSet<Resume> AllResumes { get; set; }
    }
}
