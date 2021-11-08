#include <Windows.h>
#include <SDL.h>
#include <cstdlib>
#include <string>
//=============================================================================
//	DECL :: SGWINDOW
//=============================================================================
class SGWindow {
public:
	SGWindow();
	~SGWindow();
};
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
};
//=============================================================================
//	IMPL :: SGWINDOW
//=============================================================================
SGWindow::SGWindow() {
};
SGWindow::~SGWindow() {
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
//=============================================================================
//	MAIN
//=============================================================================
int main(int argc, char* argv[]) {

	SGSystem* _system = new SGSystem();

// _BEGIN_MAIN_

	delete _system;
	return 0;
}
