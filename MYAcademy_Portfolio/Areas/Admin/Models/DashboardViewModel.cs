using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        // ── İstatistik kartları ──
        public int TotalProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int MessageCount { get; set; }
        public int UnreadMessageCount { get; set; }

        // ── Listeler ──
        public List<Message> LastMessages { get; set; } = new();     // son 5 mesaj
        public List<Project> LastProjects { get; set; } = new();     // son 3 proje
        public List<CategorySummary> CategorySummaries { get; set; } = new();   // kategori özeti
    }

    // Kategori özeti için küçük bir yardımcı sınıf: "Web → 4 proje"
    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public int ProjectCount { get; set; }
    }
}