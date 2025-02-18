# Peaky - File Preview Application  

Peaky is a modern Windows application that provides **quick and efficient file previews**. It allows users to preview various file types, including images, text files, and media files, directly from their system.  

---

## 🚀 Features  

✔️ **Modern, clean UI with a dark theme**  
✔️ **Supports multiple file types:**  
   - 📷 **Images**  
   - 📄 **Text files**  
   - 🎵 **Media files**  
✔️ **Keyboard shortcuts** for quick access ⌨️  
✔️ **Custom window controls** (minimize, maximize, close)  
✔️ **Fast file selection and preview rendering** ⚡  

---

## 🛠️ Installation  

1️⃣ **Download** the latest installer from the releases page  
2️⃣ **Run** the installer (`PeakySetup.msi`)  
3️⃣ **Follow** the installation wizard instructions  
4️⃣ **Launch Peaky** from the **Start Menu** or desktop shortcut  

---

## 📌 Usage  

Peaky integrates with your system for **quick file previews**. Once installed, you can:  

1️⃣ **Use keyboard shortcuts** to activate the preview window  
2️⃣ **Select files** to preview  
3️⃣ **Manage the preview window** using the provided controls  

---

## 💻 Development  

### 🔧 Prerequisites  

- **.NET 7.0 SDK** or later  
- **Visual Studio 2022** or later  
- **WiX Toolset v3.x** (for building the installer)  

### 📂 Project Structure  

```
Peaky/           # Main application project
  ├── Services/  # Core application services
  ├── Views/     # WPF views and windows
Peaky.Tests/     # Unit tests
Peaky.Setup/     # WiX installer project
```

### ⚙️ Building  

1️⃣ **Clone the repository**  
2️⃣ **Open** `Peaky.sln` in Visual Studio  
3️⃣ **Restore** NuGet packages  
4️⃣ **Build** the solution  

### ✅ Testing  

Peaky uses **xUnit** for testing. To run tests:  

1️⃣ Open **Test Explorer** in Visual Studio  
2️⃣ **Build** the solution  
3️⃣ **Run all tests**  

---

## 📦 Dependencies  

- **Microsoft.Extensions.DependencyInjection**  
- **CommunityToolkit.Mvvm**  
- **Serilog**  
- **xUnit** (for testing)  
- **Moq** (for testing)  

---

## 📜 License  

📝 **MIT License** - Open-source project, free to use and modify.  