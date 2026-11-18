<div align="center">

# 🌐 MYAcademy Portfolio

### Yönetim panelinden tamamen yönetilebilen, veritabanı destekli kişisel portfolyo sitesi

<p>
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
</p>
<p>
  <img src="https://img.shields.io/badge/Entity_Framework-Core-6DB33F?style=for-the-badge&logo=dotnet&logoColor=white" alt="EF Core" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white" alt="Bootstrap 5" />
  <img src="https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black" alt="JavaScript" />
</p>

**M&Y Yazılım Eğitim Akademi**  1. Proje

[Özellikler](#-özellikler) •
[Mimari](#-mimari) •
[Veritabanı](#-veritabanı-şeması) •
[Teknikler](#-uygulanan-teknikler) •
[Kurulum](#-kurulum) •
[Ekran Görüntüleri](#-ekran-görüntüleri)

</div>

---

## 💡 Proje Hakkında

Bu proje, statik bir portfolyo tasarımını **veritabanı destekli, yönetilebilir ve bütünleşik** bir web uygulamasına dönüştürür.

Sitede gördüğünüz **her şey** (isim, tanıtım yazısı, profil fotoğrafı, eğitimler, deneyimler, yetenekler, projeler, sosyal medya bağlantıları ve indirilebilir CV) **admin panelinden** yönetilir. Panelde yapılan her değişiklik sitede **anında** görünür; kodda tek satır değiştirmeye gerek kalmaz.

<table>
<tr>
<td width="50%" valign="top">

### 🖥️ Portfolyo Sitesi
Ziyaretçilerin gördüğü, tek sayfalık modern portfolyo.

`https://localhost:xxxx/`

</td>
<td width="50%" valign="top">

### ⚙️ Yönetim Paneli
İçeriklerin eklendiği, düzenlendiği ve istatistiklerin izlendiği panel.

`https://localhost:xxxx/Admin`

</td>
</tr>
</table>

---

## ✨ Özellikler

### 🎯 Öne Çıkanlar

| | Özellik | Açıklama |
|:---:|---|---|
| 📊 | **Canlı Dashboard** | 7 istatistik kartı, son mesajlar, son projeler ve kategori bazında proje grafiği. Hepsi EF Core sorgularıyla anlık hesaplanır |
| 🏷️ | **Etiketli Yetenek Formu** | Yetenekler etiket olarak eklenir; hazır öneri listesi sunulur, veritabanında olanlar önerilmez |
| 🖼️ | **Görsel Yükleme** | Proje kapakları ve profil fotoğrafı için tür/boyut kontrollü, benzersiz isimli güvenli yükleme |
| 📄 | **Akıllı CV Yükleme** | Yalnızca PDF kabul edilir. Uzantının yanında dosya imzası (`%PDF`) da kontrol edilir |
| 🛡️ | **Korumalı Silme** | İçinde proje bulunan kategori silinemez; yanlışlıkla veri kaybı engellenir |
| 🔗 | **Dinamik Sosyal Medya** | Tek bir bileşen; ana sayfa, iletişim ve footer'da aynı anda güncellenir |
| 👁️ | **Aktif / Pasif** | Pasif yapılan eğitim, deneyim ve projeler sitede otomatik gizlenir |

<details>
<summary><b>🖥️ Portfolyo Sitesi: tüm özellikler</b></summary>
<br>

- **Ana sayfa:** Ad, karşılama rozeti, tanıtım yazısı ve profil fotoğrafı veritabanından gelir
- **Hakkımda, Eğitim, Deneyim, Yetenekler, Projeler** bölümleri tamamen dinamik
- **Tarih sıralaması:** Eğitim ve deneyimler en güncelden eskiye sıralanır, "Halen" devam edenler en üstte
- **Proje filtresi:** Projeler kategori butonlarıyla filtrelenir
- **Yetenekler:** Kategoriler halinde kartlarda gösterilir
- **İletişim formu:** Ad, e-posta, konu ve mesaj doğrulanarak veritabanına kaydedilir
- **CV İndir:** Admin'den yüklenen güncel CV'yi düzgün bir dosya adıyla indirir; CV yoksa buton gizlenir
- **Menü:** Tüm bağlantılar ilgili bölümlere kaydırır

</details>

<details>
<summary><b>📊 Dashboard</b></summary>
<br>

- **İstatistik kartları:** Toplam proje · Aktif proje · Yetenek · Deneyim · Eğitim · Toplam mesaj · Okunmamış mesaj
- **Son 5 mesaj:** Gönderen, konu, tarih ve okundu durumu. Tıklanınca mesajın detayı otomatik açılır
- **Son 3 proje:** Proje adı, kategori ve aktif/pasif durumu, düzenleme bağlantısıyla
- **Kategori özeti:** Kategori bazında proje sayıları (`GroupBy`) ve oransal çubuk grafik
- **Hızlı işlemler:** Yeni proje ekle · Mesajları görüntüle · Portfolyoyu aç
- Tüm veriler tek bir **`DashboardViewModel`** ile taşınır, sabit sayı kullanılmaz

</details>

<details>
<summary><b>📁 Projeler ve Kategoriler</b></summary>
<br>

**Projeler**
- Tam CRUD: listele, ekle, güncelle, sil
- Veritabanından gelen **kategori dropdown'u** (`SelectList` / `asp-items`)
- **Çoklu yetenek seçimi:** Kullanılan teknolojiler aranabilir çipler halinde seçilir (çoka-çok ilişki)
- **Kapak görseli:** Önizlemeli yükleme; güncellemede eski dosya silinir, görsel değiştirilmezse mevcut olan korunur
- Liste sayfasında **arama**, **kategori** ve **durum** filtresi, renkli aktif/pasif etiketi

**Proje Kategorileri**
- Tam CRUD, her kategorinin yanında proje sayısı
- Aynı isimde iki kategori eklenemez
- İçinde proje olan kategori **silinemez** (hem arayüzde hem sunucuda kontrol edilir)

</details>

<details>
<summary><b>💡 Yetenek Kategorileri</b></summary>
<br>

- Tam CRUD; ikon seçimi ve canlı önizleme
- Kategori eklerken yetenekler **etiket** olarak yazılır (Enter, virgül veya "Ekle" butonu)
- `datalist` ile **öneri listesi**; tekrar eden ve boş değerler otomatik temizlenir
- Güncellemede **fark mantığı:** Sadece kaldırılan yetenekler silinir, sadece yeniler eklenir. Değişmeyen yeteneklerin proje bağlantıları korunur

</details>

<details>
<summary><b>✉️ Mesajlar</b></summary>
<br>

- **Okundu / okunmadı** sekmeleri ve sayaçları
- Gönderene, e-postaya veya konuya göre **arama**; tarihe göre **sıralama**
- Mesaj detayı **Bootstrap Modal** ile açılır ve otomatik olarak okundu işaretlenir
- Silme öncesi **SweetAlert2** onay penceresi
- Gelen mesajın orijinal içeriği düzenlenemez

</details>

<details>
<summary><b>👤 Hakkımda · ⚙️ Ayarlar · 🔗 Sosyal Medya · 🎓 Eğitim · 💼 Deneyim</b></summary>
<br>

- **Hakkımda:** Tek formdan güncellenir ("varsa güncelle, yoksa oluştur" mantığı), karakter sayaçlı açıklama alanı
- **Ayarlar:** Ana sayfa yazıları, profil fotoğrafı ve CV tek ekranda; dosyalar önizlemeli
- **Sosyal Medya:** GitHub, LinkedIn, Instagram, X, YouTube gibi platformlar için hızlı seçim; `https://` ve `mailto:` bağlantı kontrolü
- **Eğitim / Deneyim:** Aktif/pasif durumu renkli etiketle, kayıtlar tarihe göre sıralı

</details>

---

## 🏗️ Mimari

```mermaid
flowchart LR
    V([👤 Ziyaretçi]) --> S[🖥️ Portfolyo Sitesi<br/>DefaultController]
    A([👩‍💻 Yönetici]) --> P[⚙️ Admin Paneli<br/>Areas/Admin]

    S --> VC[🧩 View Component'ler<br/>Hero · Hakkımda · Projeler ...]
    S --> CF[✉️ İletişim Formu<br/>ContactController]
    P --> CT[🎛️ Controller'lar<br/>Project · Category · Message ...]

    VC --> DB[(🗄️ SQL Server)]
    CF --> DB
    CT --> DB
    CT --> FS[📁 wwwroot<br/>görseller · CV]

    style DB fill:#86a08b,color:#fff
    style FS fill:#8276d9,color:#fff
```

**Akış:** Yönetici panelden içerik ekler → veriler **EF Core** ile SQL Server'a kaydedilir → site bölümlerindeki **View Component**'ler bu verileri okuyup sayfayı oluşturur.

---

## 🗄️ Veritabanı Şeması

```mermaid
erDiagram
    CATEGORY ||--o{ PROJECT : "içerir"
    SKILLCATEGORY ||--o{ SKILL : "içerir"
    PROJECT }o--o{ SKILL : "kullanır"
    EXPERIENCE }o--o{ SKILL : "kullanır"

    CATEGORY {
        int CategoryId PK
        string CategoryName
    }
    PROJECT {
        int ProjectId PK
        string Title
        string Description
        string GithubUrl
        string CoverImageUrl
        bool IsActive
        int CategoryId FK
    }
    SKILLCATEGORY {
        int SkillCategoryId PK
        string Name
        string Description
        string Icon
    }
    SKILL {
        int SkillId PK
        string Name
        int SkillCategoryId FK
    }
    EXPERIENCE {
        int ExperienceId PK
        string Title
        string CompanyName
        string Duration
        bool IsActive
    }
```

| İlişki | Tür | Açıklama |
|---|---|---|
| `Category` → `Project` | **Bire-çok** | Her projenin bir kategorisi vardır (FK + Navigation Property) |
| `SkillCategory` → `Skill` | **Bire-çok** | Her yetenek bir kategoriye aittir |
| `Project` ↔ `Skill` | **Çoka-çok** | Bir projede birçok yetenek, bir yetenek birçok projede |
| `Experience` ↔ `Skill` | **Çoka-çok** | EF Core ara tabloyu otomatik oluşturur |

> Diğer tablolar: `About`, `HeroSection` (ana sayfa + CV yolu), `Contact`, `SocialMedia`, `Message`, `Education`

---

## 🧠 Uygulanan Teknikler

<table>
<tr>
<td width="50%" valign="top">

#### 🗃️ Veri Katmanı
- **Code First** yaklaşımı ve **Migrations**
- Mevcut veriyi koruyan *data migration* (`migrationBuilder.Sql`)
- Bire-çok ve çoka-çok ilişkiler
- LINQ: `Include` · `Where` · `Select` · `Count` · `Any` · `OrderByDescending` · `Take` · `GroupBy` · `Distinct`

#### ✅ Doğrulama
- **Data Annotations:** `Required` · `StringLength` · `Range` · `EmailAddress` · `Url` · `ValidateNever`
- `ModelState.IsValid` ile sunucu taraflı kontrol
- `AddModelError` ile özel kurallar (aynı isim, dosya türü…)
- Türkçe hata mesajları

</td>
<td width="50%" valign="top">

#### 🧩 Yapı
- **ViewModel'ler:** `DashboardViewModel` · `ProjectFormViewModel` · `SkillCategoryFormViewModel`
- **View Component'ler:**
  - Site: Navbar · Hero · Hakkımda · Eğitim · Deneyim · Yetenek · Proje · İletişim · Footer · Sosyal Medya
  - Admin: Head · Style · Sidebar · Script
- Tek bileşenin birden çok yerde kullanımı (Sosyal Medya)

#### 🔒 Dosya & Güvenlik
- `IFormFile` + `IWebHostEnvironment`
- Tür, boyut ve **dosya imzası** kontrolü
- `Guid` ile benzersiz dosya adları
- Sadece izinli klasörlerde silme
- Tüm formlarda `ValidateAntiForgeryToken`

</td>
</tr>
</table>

#### 🔔 Kullanıcı Deneyimi
`TempData` + **SweetAlert2** ile işlem bildirimleri · **Bootstrap Modal** ile detay görünümü · arama ve filtreler · görsel önizleme · boş liste mesajları · responsive tasarım

---

## 📂 Proje Yapısı

```
MYAcademy_Portfolio/
│
├── 📁 Areas/Admin/
│   ├── Controllers/       → Dashboard · Project · Category · SkillCategory · Message
│   │                        About · Education · Experience · SocialMedia · Setting
│   ├── Models/            → DashboardViewModel · ProjectFormViewModel · SkillCategoryFormViewModel
│   ├── ViewComponents/    → Admin layout bileşenleri
│   └── Views/             → Admin sayfaları
│
├── 📁 Controllers/        → DefaultController (site) · ContactController (iletişim formu)
├── 📁 Context/            → PortfolioContext (DbContext)
├── 📁 Entities/           → Veritabanı tabloları
├── 📁 Helpers/            → DurationHelper ("2021 - Halen" → sıralama için yıl çıkarma)
├── 📁 Migrations/         → EF Core migration geçmişi
├── 📁 ViewComponents/     → Site bileşenleri
├── 📁 Views/              → Site sayfası ve bileşen görünümleri
└── 📁 wwwroot/            → CSS, JS, yüklenen görseller ve CV
```

---

## 🙏 Teşekkür

Bu projenin ortaya çıkmasında emeği olan, **ASP.NET Core MVC** ve **Entity Framework Core** temellerini öğreten ve eğitim boyunca desteğini esirgemeyen değerli hocam **Murat Yücedağ**'a ve **M&Y Yazılım Eğitim Akademi** ailesine içtenlikle teşekkür ederim. 💜

---

<div align="center">

## 👩‍💻 Geliştirici

**Sara Ebrar Acar**
Bilgisayar Mühendisi · Full Stack Developer

<a href="https://github.com/sebraracr">
  <img src="https://img.shields.io/badge/GitHub-sebraracr-181717?style=for-the-badge&logo=github&logoColor=white" alt="GitHub" />
</a>

<br><br>

⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!

</div>

<img width="1287" height="928" alt="Ekran görüntüsü 2026-11-18 152839" src="https://github.com/user-attachments/assets/1767617b-e520-4499-be2f-4170d117413d" />
<img width="1906" height="933" alt="Ekran görüntüsü 2026-11-18 145533" src="https://github.com/user-attachments/assets/fe419e77-c649-4289-9e52-a767cfb8a40f" />
<img width="1160" height="950" alt="Ekran görüntüsü 2026-11-18 145618" src="https://github.com/user-attachments/assets/e84413c1-1700-486d-815d-c7df396966a4" />
<img width="1432" height="939" alt="Ekran görüntüsü 2026-11-18 145633" src="https://github.com/user-attachments/assets/13c8d0d2-b1b8-4d23-98d3-a9ffa7dadead" />
<img width="680" height="854" alt="Ekran görüntüsü 2026-11-18 145654" src="https://github.com/user-attachments/assets/fd41a2d6-bffd-47e6-a8f9-dd3bae6f2696" />
<img width="1055" height="685" alt="Ekran görüntüsü 2026-11-18 145706" src="https://github.com/user-attachments/assets/92c2089e-f27e-4df9-a68b-7e8e2d7af2d5" />
<img width="1105" height="724" alt="Ekran görüntüsü 2026-11-18 145731" src="https://github.com/user-attachments/assets/e2bd63cd-7c5b-4323-aa53-055a2fc229c5" />
<img width="1015" height="720" alt="Ekran görüntüsü 2026-11-18 145752" src="https://github.com/user-attachments/assets/bdec2ab0-05bf-484c-9dd4-9274854d30a3" />
<img width="1251" height="946" alt="Ekran görüntüsü 2026-11-18 145821" src="https://github.com/user-attachments/assets/aeccebf5-c39c-49d4-96a7-b47fe89e1a55" />
<img width="1911" height="938" alt="Ekran görüntüsü 2026-11-18 145845" src="https://github.com/user-attachments/assets/e83608f6-6030-4b5d-afd0-c6f1c4384118" />
<img width="1905" height="935" alt="Ekran görüntüsü 2026-11-18 145857" src="https://github.com/user-attachments/assets/5ad8bd05-2682-42c8-890b-a3bfb1d4c56f" />
<img width="1909" height="940" alt="Ekran görüntüsü 2026-11-18 145912" src="https://github.com/user-attachments/assets/82d7eb79-cecd-477f-8829-3c7ccc4b2d0e" />
<img width="1899" height="939" alt="Ekran görüntüsü 2026-11-18 145933" src="https://github.com/user-attachments/assets/f694cc35-e8fe-40c6-93eb-e1227a08a0b5" />
<img width="1911" height="947" alt="Ekran görüntüsü 2026-11-18 145948" src="https://github.com/user-attachments/assets/5672456c-cb0b-41b7-8bb1-98d94d93351d" />
<img width="1906" height="944" alt="Ekran görüntüsü 2026-11-18 150021" src="https://github.com/user-attachments/assets/f81b3f56-a5dd-472f-a6b3-642ea2ffaeac" />
<img width="1912" height="934" alt="Ekran görüntüsü 2026-11-18 150034" src="https://github.com/user-attachments/assets/eaae28c8-48ef-411e-9c73-db76ce854738" />
<img width="1910" height="930" alt="Ekran görüntüsü 2026-11-18 150048" src="https://github.com/user-attachments/assets/df6ede17-b1e5-4203-8a48-ace17e715d70" />
<img width="1914" height="941" alt="Ekran görüntüsü 2026-11-18 150058" src="https://github.com/user-attachments/assets/3df75f90-9724-44b9-b36c-ac533b5267c6" />
<img width="1907" height="940" alt="Ekran görüntüsü 2026-11-18 150106" src="https://github.com/user-attachments/assets/049f717f-a711-4b00-b650-fe26c365a437" />
<img width="1905" height="942" alt="Ekran görüntüsü 2026-11-18 150115" src="https://github.com/user-attachments/assets/aa1220b3-f116-43f8-baa7-34c30a91ed7c" />

