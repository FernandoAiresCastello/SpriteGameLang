#include <Windows.h>
#include <SDL.h>
#include <cstdlib>
#include <string>
#include <vector>
#include <map>
#include <iostream>
#include <fstream>

/******************************************************************************

							   ENUMERATIONS

******************************************************************************/
enum class MsgBoxType {
	Info, Warning, Error
};
/******************************************************************************

							CLASS DECLARATIONS

******************************************************************************/

//=============================================================================
//	DECL :: SGUtil
//=============================================================================
class SGUtil {
public:
	static void ShowMsgBox(std::string title, std::string message, MsgBoxType type);
};
//=============================================================================
//	DECL :: SGString
//=============================================================================
class SGString {
public:
	static std::string Format(const char* fmt, ...);
};
//=============================================================================
//	DECL :: SGFile
//=============================================================================
class SGFile {
public:
	static bool Exists(std::string file);
};
//=============================================================================
//	DECL :: SGSystem
//=============================================================================
class SGSystem {
public:
	SGSystem();
	~SGSystem();
	
	void Init();
	void Exit();
	void ProcessGlobalEvents();
	void Halt();
	void SetFileRoot(std::string root);
	static void Abort(std::string message);
	std::string GetFileRoot();

private:
	std::string FileRoot = "";
};
//=============================================================================
//	DECL :: SGImage
//=============================================================================
class SGImage {
public:
	SGImage(SDL_Renderer* rend, std::string file);
	~SGImage();

	SDL_Texture* Texture = nullptr;
	int Width;
	int Height;
};
//=============================================================================
//	DECL :: SGWindow
//=============================================================================
class SGWindow {
public:
	SGWindow();
	~SGWindow();

	void Open(int width, int height, bool full);
	void SetTitle(std::string title);
	void Close();
	void Clear(int rgb);
	void Update();
	SDL_Renderer* GetRenderer();
	void DrawImage(SGImage* img, int x, int y);

private:
	SDL_Window* Wnd = nullptr;
	SDL_Renderer* Rend = nullptr;
	int WndWidth = 0;
	int WndHeight = 0;
	bool Full = false;
	std::string Title = "";
};
//=============================================================================
//	DECL :: SGImagePool
//=============================================================================
class SGImagePool {
public:
	SGImagePool();
	~SGImagePool();

	void SetRenderer(SDL_Renderer* rend);
	void Load(std::string id, std::string file);
	SGImage* Get(std::string id);

private:
	SDL_Renderer* Rend = nullptr;
	std::map<std::string, SGImage*> Images;
};
//=============================================================================
//	DECL :: SGApiContext
//=============================================================================
class SGApiContext {
public:
	SGApiContext();
	~SGApiContext();

	void Test();
	void ShowMsgBox(std::string message);
	void Exit();
	void SetWindowTitle(std::string title);
	void OpenWindow(int width, int height, bool full);
	void Halt();
	void SetFileRoot(std::string path);
	void LoadImageFile(std::string id, std::string file);
	void ProcessGlobalEvents();
	void DrawImage(std::string id, int x, int y);
	void UpdateWindow();
	void ClearWindow(int rgb);

	SGSystem* System = nullptr;
	SGWindow* Window = nullptr;
	SGImagePool* ImgPool = nullptr;
};
/******************************************************************************

							CLASS IMPLEMENTATIONS

******************************************************************************/

//=============================================================================
//	IMPL :: SGApiContext
//=============================================================================
SGApiContext::SGApiContext() {
	System = new SGSystem();
	Window = new SGWindow();
	ImgPool = new SGImagePool();
}
SGApiContext::~SGApiContext() {
	delete ImgPool;
	delete Window;
	delete System;
}
void SGApiContext::Test() {
}
void SGApiContext::ShowMsgBox(std::string message) {
	SGUtil::ShowMsgBox("Sprite Game API", message, MsgBoxType::Info);
}
void SGApiContext::Exit() {
	System->Exit();
}
void SGApiContext::SetWindowTitle(std::string title) {
	Window->SetTitle(title);
}
void SGApiContext::OpenWindow(int width, int height, bool full) {
	Window->Open(width, height, full);
	ImgPool->SetRenderer(Window->GetRenderer());
}
void SGApiContext::Halt() {
	System->Halt();
}
void SGApiContext::SetFileRoot(std::string path) {
	System->SetFileRoot(path);
}
void SGApiContext::LoadImageFile(std::string id, std::string file) {
	ImgPool->Load(id, System->GetFileRoot() + file);
}
void SGApiContext::ProcessGlobalEvents() {
	System->ProcessGlobalEvents();
}
void SGApiContext::DrawImage(std::string id, int x, int y) {
	SGImage* img = ImgPool->Get(id);
	if (img != nullptr) {
		Window->DrawImage(img, x, y);
	}
}
void SGApiContext::UpdateWindow() {
	Window->Update();
}
void SGApiContext::ClearWindow(int rgb) {
	Window->Clear(rgb);
}
//=============================================================================
//	IMPL :: SGUtil
//=============================================================================
void SGUtil::ShowMsgBox(std::string title, std::string message, MsgBoxType type) {
	long icon = 0;
	if (type == MsgBoxType::Info)
		icon = MB_ICONINFORMATION;
	else if (type == MsgBoxType::Warning)
		icon = MB_ICONWARNING;
	if (type == MsgBoxType::Error)
		icon = MB_ICONERROR;

	MessageBoxA(nullptr, message.c_str(), title.c_str(),
		MB_OK | MB_TASKMODAL | MB_SETFOREGROUND | icon);
}
//=============================================================================
//	IMPL :: SGString
//=============================================================================
std::string SGString::Format(const char* fmt, ...) {
	char str[1000] = { 0 };
	va_list arg;
	va_start(arg, fmt);
	vsprintf(str, fmt, arg);
	va_end(arg);

	return str;
}
//=============================================================================
//	IMPL :: SGFile
//=============================================================================
bool SGFile::Exists(std::string file) {
	if (FILE *fp = fopen(file.c_str(), "r")) {
		fclose(fp);
		return true;
	}
	return false;
}
//=============================================================================
//	IMPL :: SGImagePool
//=============================================================================
SGImagePool::SGImagePool() {
}
SGImagePool::~SGImagePool() {
	for (auto it = Images.begin(); it != Images.end(); it++) {
		delete it->second;
		it->second = nullptr;
	}
	Images.clear();
}
void SGImagePool::SetRenderer(SDL_Renderer* rend) {
	Rend = rend;
}
void SGImagePool::Load(std::string id, std::string file) {
	Images[id] = new SGImage(Rend, file);
}
SGImage* SGImagePool::Get(std::string id) {
	if (Images.find(id) == Images.end()) {
		SGSystem::Abort("Image not found with id: " + id);
		return nullptr;
	}
	return Images[id];
}
//=============================================================================
//	IMPL :: SGImage
//=============================================================================
SGImage::SGImage(SDL_Renderer* rend, std::string file) {
	if (!SGFile::Exists(file)) {
		SGSystem::Abort("File not found: " + file);
		return;
	}
	SDL_Surface* sfc = SDL_LoadBMP(file.c_str());
	if (!sfc) {
		SGSystem::Abort("Could not load file: " + file);
		return;
	}
	if (!rend) {
		SGSystem::Abort("Renderer is not initialized");
		return;
	}
	Texture = SDL_CreateTextureFromSurface(rend, sfc);
	if (!Texture) {
		std::string error = SGString::Format(
			"Texture creation failed for file: %s\n\n%s",
			file.c_str(), SDL_GetError());
		SGSystem::Abort(error);
		return;
	}
	Width = sfc->w;
	Height = sfc->h;
	SDL_FreeSurface(sfc);
}
SGImage::~SGImage() {
	SDL_DestroyTexture(Texture);
	Texture = nullptr;
}
//=============================================================================
//	IMPL :: SGSystem
//=============================================================================
SGSystem::SGSystem() {
	Init();
};
SGSystem::~SGSystem() {
	Exit();
};
void SGSystem::Init() {
	SDL_Init(SDL_INIT_EVERYTHING);
}
void SGSystem::Exit() {
	SDL_Quit();
	exit(0);
}
void SGSystem::ProcessGlobalEvents() {
	SDL_Event e;
	SDL_PollEvent(&e);
	if (e.type == SDL_QUIT) {
		Exit();
	}
}
void SGSystem::Halt() {
	while (true) {
		ProcessGlobalEvents();
		SDL_Delay(1);
	}
}
void SGSystem::SetFileRoot(std::string root) {
	char ixLastChar = root.length() - 1;
	if (root[ixLastChar] != '\\' && root[ixLastChar] != '/') {
		root = root.append("\\");
	}
	FileRoot = root;
}
void SGSystem::Abort(std::string message) {
	SGUtil::ShowMsgBox("Sprite Game API", message, MsgBoxType::Error);
	SDL_Quit();
	exit(0);
}
std::string SGSystem::GetFileRoot() {
	return FileRoot;
}
//=============================================================================
//	IMPL :: SGWindow
//=============================================================================
SGWindow::SGWindow() {
};
SGWindow::~SGWindow() {
	Close();
};
void SGWindow::Open(int width, int height, bool full) {
	WndWidth = width;
	WndHeight = height;
	Full = full;

	SDL_SetHint(SDL_HINT_RENDER_DRIVER, "direct3d");
	SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "nearest");

	Wnd = SDL_CreateWindow("",
		SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
		WndWidth, WndHeight, Full ? SDL_WINDOW_FULLSCREEN_DESKTOP : 0);

	Rend = SDL_CreateRenderer(Wnd, -1,
		SDL_RENDERER_PRESENTVSYNC | SDL_RENDERER_ACCELERATED | SDL_RENDERER_TARGETTEXTURE);

	Clear(0xffffff);
	SetTitle(Title);
	SDL_SetWindowPosition(Wnd, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED);
	SDL_RaiseWindow(Wnd);
}
void SGWindow::SetTitle(std::string title) {
	Title = title;
	if (Wnd)
		SDL_SetWindowTitle(Wnd, Title.c_str());
}
void SGWindow::Close() {
	SDL_DestroyRenderer(Rend);
	Rend = nullptr;
	SDL_DestroyWindow(Wnd);
	Wnd = nullptr;
}
void SGWindow::Clear(int rgb) {
	const int r = (rgb & 0xff0000) >> 16;
	const int g = (rgb & 0x00ff00) >> 8;
	const int b = (rgb & 0x0000ff);
	SDL_SetRenderDrawColor(Rend, r, g, b, SDL_ALPHA_OPAQUE);
	SDL_RenderClear(Rend);
}
void SGWindow::Update() {
	SDL_RenderPresent(Rend);
}
SDL_Renderer* SGWindow::GetRenderer() {
	return Rend;
}
void SGWindow::DrawImage(SGImage* img, int x, int y) {
	SDL_Rect src;
	src.x = 0;
	src.y = 0;
	src.w = img->Width;
	src.h = img->Height;
	SDL_Rect dst;
	dst.x = x;
	dst.y = y;
	dst.w = img->Width;
	dst.h = img->Height;
	SDL_RenderCopy(Rend, img->Texture, &src, &dst);
}
//=============================================================================
//	MAIN
//=============================================================================
SGApiContext* _api = nullptr;

int main(int argc, char* argv[]) {

	_api = new SGApiContext();

// _BEGIN_MAIN_

	delete _api;
	return 0;
}
