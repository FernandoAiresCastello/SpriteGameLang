#include <Windows.h>
#include <SDL.h>
#include <cstdlib>
#include <string>
//=============================================================================
//	DECL :: SGSYSTEM
//=============================================================================
class SGSystem {
public:
	SGSystem();
	~SGSystem();
	
	void Init();
	void Exit();
	void ShowMsgBox(std::string message);
	void ProcessGlobalEvents();
	void Halt();
};
//=============================================================================
//	DECL :: SGWINDOW
//=============================================================================
class SGWindow {
public:
	SGWindow();
	~SGWindow();

	void Open(int width, int height, bool full);
	void SetTitle(std::string title);

private:
	SDL_Window* Wnd = nullptr;
	SDL_Renderer* Rend = nullptr;
	int WndWidth = 0;
	int WndHeight = 0;
	bool Full = false;
	std::string Title = "";
};
//=============================================================================
//	IMPL :: SGSYSTEM
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
void SGSystem::ShowMsgBox(std::string message) {
	MessageBoxA(nullptr, message.c_str(), "Sprite Game API",
		MB_OK | MB_ICONINFORMATION | MB_TASKMODAL | MB_SETFOREGROUND);
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
	}
}
//=============================================================================
//	IMPL :: SGWINDOW
//=============================================================================
SGWindow::SGWindow() {
};
SGWindow::~SGWindow() {
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

	SetTitle(Title);
	SDL_SetWindowPosition(Wnd, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED);
	SDL_RaiseWindow(Wnd);
}
void SGWindow::SetTitle(std::string title) {
	Title = title;
	if (Wnd)
		SDL_SetWindowTitle(Wnd, Title.c_str());
}
//=============================================================================
//	MAIN
//=============================================================================
int main(int argc, char* argv[]) {

	SGSystem* _system = new SGSystem();
	SGWindow* _window = new SGWindow();

// _BEGIN_MAIN_

	delete _window;
	delete _system;
	return 0;
}
