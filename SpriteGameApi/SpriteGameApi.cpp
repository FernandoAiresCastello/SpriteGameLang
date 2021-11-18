#include <Windows.h>
#include <SDL.h>
#include <cstdlib>
#include <string>
#include <vector>
#include <map>
#include <iostream>
#include <fstream>
#include <cstdarg>
#include <sstream>
#include <algorithm>
#include <cctype>
#include <bitset>

/// Index..
class SGApiContext;
enum class SGMsgBoxType;
class SGUtil;
class SGPosition;
class SGString;
class SGFile;
class SGSystem;
class SGWindow;
class SGImage;
class SGImagePool;
class SGTileset;
class SGSprite;
class SGParameter;
class SGVariable;
/// SGVariable..
class SGVariable {
public:
	std::string Name = "";
	std::string StringValue = "";
	int NumberValue = 0;
	bool BoolValue = false;
};
/// SGParameter..
class SGParameter {
public:
	std::string StringValue = "";
	int NumberValue = 0;
	bool BoolValue = false;
};
/// SGLayer..
class SGLayer {
public:
	bool Enabled = true;
	std::vector<SGSprite> Sprites;
	int ScrollX = 0;
	int ScrollY = 0;

	void Clear();
	void AddSprite(SGImage* img, int x, int y);
	void AddSprite(SGImage* img, int tileW, int tileH, int srcX, int srcY, int dstX, int dstY);
};
/// SGSprite..
class SGSprite {
public:
	SGImage* Image;
	SDL_Rect Src;
	SDL_Rect Dst;
};
/// SGPosition..
class SGPosition {
public:
	int X = 0;
	int Y = 0;
};
/// SGMsgBoxType..
enum class SGMsgBoxType {
	Info, Warning, Error
};
/// SGUtil..
class SGUtil {
public:
	static void ShowMsgBox(std::string title, std::string message, SGMsgBoxType type);
};
/// SGString..
class SGString {
public:
	static std::string Format(const char* fmt, ...);
	static int ToInt(std::string str);
	static std::string ToString(int x);
	static std::string Skip(std::string text, int count);
};
/// SGFile..
class SGFile {
public:
	static bool Exists(std::string file);
};
/// SGSystem..
class SGWindow;
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

	std::string FileRoot = "";
	SGWindow* Window = nullptr;
};
/// SGImage..
class SGImage {
public:
	SGImage(SDL_Renderer* rend, std::string file, int transparencyKey);
	~SGImage();

	SDL_Texture* Texture = nullptr;
	int Width = 0;
	int Height = 0;
	int TransparencyKey = 0;
};
/// SGTileset..
class SGTileset {
public:
	SGTileset(SGImage* image, int tileWidth, int tileHeight);
	~SGTileset();

	int GetTileXFromIndex(int index);
	int GetTileYFromIndex(int index);

	SGImage* Image = nullptr;
	int TileWidth = 0;
	int TileHeight = 0;
	int Cols = 0;
	int Rows = 0;

private:
	std::vector<SGPosition> TilePositions;

	void CalculateTilePositions();
};
/// SGWindow..
class SGWindow {
public:
	SGWindow();
	~SGWindow();

	void Open(int hRes, int vRes, int layers, int sizeMultiplier);
	void Open(int hRes, int vRes, int layers, int hSize, int vSize);
	void SetTitle(std::string title);
	void Close();
	void Clear();
	void Update();
	SDL_Renderer* GetRenderer();
	void SetFullscreen(bool full);
	void ToggleFullscreen();
	void AssertLayerIndex(int index);
	void SelectLayer(int index);
	void EnableLayer(int index, bool enable);
	void ScrollLayerToPoint(int index, int x, int y);
	void DrawImage(SGImage* img, int x, int y);
	void DrawTile(SGImage* img, int tileW, int tileH, int srcX, int srcY, int dstX, int dstY);
	void DeleteAllSprites();
	void DeleteSprites(int ixLayer);

	int BackColor = 0xffffff;

private:
	SDL_Window* Wnd = nullptr;
	SDL_Renderer* Rend = nullptr;
	int ResWidth = 0;
	int ResHeight = 0;
	int WndWidth = 0;
	int WndHeight = 0;
	bool Full = false;
	std::string Title = "";
	int SelectedLayer = 0;
	std::vector<SGLayer> Layers;
};
/// SGImagePool..
class SGImagePool {
public:
	SGImagePool();
	~SGImagePool();

	void SetRenderer(SDL_Renderer* rend);
	void Load(std::string id, std::string file);
	SGImage* Get(std::string id);

	int TransparencyKey;

private:
	SDL_Renderer* Rend = nullptr;
	std::map<std::string, SGImage*> Images;
};
/// SGApiContext..
class SGApiContext {
public:
	SGApiContext();
	~SGApiContext();
	
	// Commands
	void Test();
	void ShowMsgBox();
	void Exit();
	void SetWindowTitle();
	void OpenWindow();
	void SetFullscreen();
	void Halt();
	void SetFileRoot();
	void LoadImageFile();
	void ProcessGlobalEvents();
	void UpdateWindow();
	void SetTransparencyKey();
	void SetWindowBackColor();
	void DeleteAllSprites();
	void DeleteSprites();
	void MakeTileset();
	void SelectLayer();
	void EnableLayer();
	void ScrollLayerToPoint();
	void DrawImage();
	void DrawTile();
	void DrawString();
	void IncrementVariable();
	void DecrementVariable();

	// Internals
	void AddFunctionCallArgument(std::string arg);
	void SetVariable(std::string name, int numberValue);
	void SetVariable(std::string name, std::string stringValue);
	void SetVariablesEqual(std::string name1, std::string name2);
	SGVariable* GetVariable(std::string name);

	SGSystem* System = nullptr;
	SGWindow* Window = nullptr;
	SGImagePool* ImgPool = nullptr;
	std::map<std::string, SGTileset*> Tilesets;
	std::vector<SGParameter> Args;
	std::map<std::string, SGVariable> Vars;

private:
	SGTileset* GetTileset(std::string id);
};

/// SGApiContext...
SGApiContext::SGApiContext() {
	System = new SGSystem();
	Window = new SGWindow();
	System->Window = Window;
	ImgPool = new SGImagePool();
}
SGApiContext::~SGApiContext() {
	delete ImgPool;
	delete Window;
	delete System;

	for (auto it = Tilesets.begin(); it != Tilesets.end(); it++) {
		delete it->second;
		it->second = nullptr;
	}
	Tilesets.clear();
}
void SGApiContext::Test() {
}
void SGApiContext::AddFunctionCallArgument(std::string arg) {
	SGParameter param;
	param.NumberValue = SGString::ToInt(arg);
	param.StringValue = arg;
	param.BoolValue = param.NumberValue > 0;
	Args.push_back(param);
}
void SGApiContext::SetVariable(std::string name, int numberValue) {
	SGVariable var;
	var.Name = name;
	var.NumberValue = numberValue;
	var.StringValue = SGString::ToString(numberValue);
	var.BoolValue = var.NumberValue > 0;
	Vars[name] = var;
}
void SGApiContext::SetVariable(std::string name, std::string stringValue) {
	SGVariable var;
	var.Name = name;
	var.NumberValue = SGString::ToInt(stringValue);
	var.StringValue = stringValue;
	var.BoolValue = var.NumberValue > 0;
	Vars[name] = var;
}
void SGApiContext::SetVariablesEqual(std::string name1, std::string name2) {
	if (Vars.find(name2) != Vars.end()) {
		SGVariable var1;
		var1.Name = name1;
		var1.NumberValue = Vars[name2].NumberValue;
		var1.StringValue = Vars[name2].StringValue;
		var1.BoolValue = Vars[name2].BoolValue;
		Vars[name1] = var1;
	}
	else {
		SGSystem::Abort("Undefined variable: " + name2);
	}
}
SGVariable* SGApiContext::GetVariable(std::string name) {
	if (Vars.find(name) != Vars.end()) {
		return &Vars[name];
	}
	SGSystem::Abort("Undefined variable: " + name);
	return nullptr;
}
void SGApiContext::ShowMsgBox() {
	std::string message = Args[0].StringValue;
	SGUtil::ShowMsgBox("", message, SGMsgBoxType::Info);
}
void SGApiContext::Exit() {
	System->Exit();
}
void SGApiContext::SetWindowTitle() {
	std::string title = Args[0].StringValue;
	Window->SetTitle(title);
}
void SGApiContext::OpenWindow() {
	int hRes = Args[0].NumberValue;
	int vRes = Args[1].NumberValue;
	int layers = Args[2].NumberValue;
	int sizeMultiplier = Args[3].NumberValue;

	Window->Open(hRes, vRes, layers, sizeMultiplier);
	ImgPool->SetRenderer(Window->GetRenderer());
}
void SGApiContext::SetFullscreen() {
	bool full = Args[0].BoolValue;
	Window->SetFullscreen(full);
}
void SGApiContext::Halt() {
	System->Halt();
}
void SGApiContext::SetFileRoot() {
	std::string path = Args[0].StringValue;
	System->SetFileRoot(path);
}
void SGApiContext::LoadImageFile() {
	std::string id = Args[0].StringValue;
	std::string file = Args[1].StringValue;
	ImgPool->Load(id, System->FileRoot + file);
}
void SGApiContext::ProcessGlobalEvents() {
	System->ProcessGlobalEvents();
}
void SGApiContext::UpdateWindow() {
	Window->Update();
}
void SGApiContext::SetTransparencyKey() {
	int rgb = Args[0].NumberValue;
	ImgPool->TransparencyKey = rgb;
}
void SGApiContext::SetWindowBackColor() {
	int rgb = Args[0].NumberValue;
	Window->BackColor = rgb;
}
void SGApiContext::DeleteAllSprites() {
	Window->DeleteAllSprites();
}
void SGApiContext::DeleteSprites() {
	int index = Args[0].NumberValue;
	Window->DeleteSprites(index);
}
void SGApiContext::SelectLayer() {
	int index = Args[0].NumberValue;
	Window->SelectLayer(index);
}
void SGApiContext::EnableLayer() {
	int index = Args[0].NumberValue;
	bool enable = Args[1].BoolValue;
	Window->EnableLayer(index, enable);
}
void SGApiContext::ScrollLayerToPoint() {
	int index = Args[0].NumberValue;
	int x = Args[1].NumberValue;
	int y = Args[2].NumberValue;
	Window->ScrollLayerToPoint(index, x, y);
}
void SGApiContext::DrawImage() {
	std::string id = Args[0].StringValue;
	int x = Args[1].NumberValue;
	int y = Args[2].NumberValue;
	SGImage* img = ImgPool->Get(id);
	if (img) {
		Window->DrawImage(img, x, y);
	}
}
void SGApiContext::MakeTileset() {
	std::string idTileset = Args[0].StringValue;
	std::string idImage = Args[1].StringValue;
	int tileWidth = Args[2].NumberValue;
	int tileHeight = Args[3].NumberValue;
	SGImage* img = ImgPool->Get(idImage);
	if (img) {
		SGTileset* tset = new SGTileset(img, tileWidth, tileHeight);
		Tilesets[idTileset] = tset;
	}
}
SGTileset* SGApiContext::GetTileset(std::string id) {
	if (Tilesets.find(id) == Tilesets.end()) {
		SGSystem::Abort("Tileset not found with id: " + id);
		return nullptr;
	}
	return Tilesets[id];
}
void SGApiContext::DrawTile() {
	std::string idTileset = Args[0].StringValue;
	int ixTile = Args[1].NumberValue;
	int x = Args[2].NumberValue;
	int y = Args[3].NumberValue;
	SGTileset* tset = GetTileset(idTileset);
	if (!tset)
		return;

	Window->DrawTile(tset->Image, tset->TileWidth, tset->TileHeight, 
		tset->GetTileXFromIndex(ixTile), tset->GetTileYFromIndex(ixTile), x, y);
}
void SGApiContext::DrawString() {
	std::string idTileset = Args[0].StringValue;
	std::string text = Args[1].StringValue;
	int x = Args[2].NumberValue;
	int y = Args[3].NumberValue;
	SGTileset* tset = GetTileset(idTileset);
	if (!tset)
		return;

	const int px = x;

	for (auto& ch : text) {
		if (ch == '\n') {
			x = px;
			y += tset->TileHeight;
		}
		else {
			Window->DrawTile(tset->Image, tset->TileWidth, tset->TileHeight,
				tset->GetTileXFromIndex(ch), tset->GetTileYFromIndex(ch), x, y);

			x += tset->TileWidth;
		}
	}
}
void SGApiContext::IncrementVariable() {
	SGVariable* var = GetVariable(Args[0].StringValue);
	SetVariable(var->Name, var->NumberValue + 1);
}
void SGApiContext::DecrementVariable() {
	SGVariable* var = GetVariable(Args[0].StringValue);
	SetVariable(var->Name, var->NumberValue - 1);
}
/// SGTileset...
SGTileset::SGTileset(SGImage* image, int tileWidth, int tileHeight) {
	Image = image;
	TileWidth = tileWidth;
	TileHeight = tileHeight;
	Cols = Image->Width / TileWidth;
	Rows = Image->Height / TileHeight;

	CalculateTilePositions();
}
SGTileset::~SGTileset() {
}
void SGTileset::CalculateTilePositions() {
	TilePositions.clear();

	for (int y = 0; y < Rows; y++) {
		for (int x = 0; x < Cols; x++) {
			SGPosition pos;
			pos.X = x * TileWidth;
			pos.Y = y * TileHeight;
			TilePositions.push_back(pos);
		}
	}
}
int SGTileset::GetTileXFromIndex(int index) {
	if (index >= 0 && index < TilePositions.size())
		return TilePositions[index].X;

	return -1;
}

int SGTileset::GetTileYFromIndex(int index) {
	if (index >= 0 && index < TilePositions.size())
		return TilePositions[index].Y;

	return -1;
}
/// SGUtil...
void SGUtil::ShowMsgBox(std::string title, std::string message, SGMsgBoxType type) {
	long icon = 0;
	if (type == SGMsgBoxType::Info)
		icon = MB_ICONINFORMATION;
	else if (type == SGMsgBoxType::Warning)
		icon = MB_ICONWARNING;
	if (type == SGMsgBoxType::Error)
		icon = MB_ICONERROR;

	MessageBoxA(nullptr, message.c_str(), title.c_str(),
		MB_OK | MB_TASKMODAL | MB_SETFOREGROUND | icon);
}
/// SGString...
std::string SGString::Format(const char* fmt, ...) {
	char str[1000] = { 0 };
	va_list arg;
	va_start(arg, fmt);
	vsprintf(str, fmt, arg);
	va_end(arg);

	return str;
}
int SGString::ToInt(std::string str) {
	int value = 0;

	bool sign = str[0] == '-';
	if (sign)
		str = SGString::Skip(str, 1);

	if (str[0] == '0' && str[1] == 'x')
		sscanf(str.c_str(), "%x", &value);
	else if (str[0] == '0' && str[1] == 'b')
		value = stoi(SGString::Skip(str, 2), nullptr, 2);
	else
		value = atoi(str.c_str());

	return sign ? -value : value;
}
std::string SGString::ToString(int x)
{
	return SGString::Format("%i", x);
}
std::string SGString::Skip(std::string text, int count)
{
	return text.substr(count);
}
/// SGFile...
bool SGFile::Exists(std::string file) {
	if (FILE *fp = fopen(file.c_str(), "r")) {
		fclose(fp);
		return true;
	}
	return false;
}
/// SGImagePool...
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
	Images[id] = new SGImage(Rend, file, TransparencyKey);
}
SGImage* SGImagePool::Get(std::string id) {
	if (Images.find(id) == Images.end()) {
		SGSystem::Abort("Image not found with id: " + id);
		return nullptr;
	}
	return Images[id];
}
/// SGImage...
SGImage::SGImage(SDL_Renderer* rend, std::string file, int transparencyKey) {
	if (!SGFile::Exists(file)) {
		SGSystem::Abort("File not found: " + file);
		return;
	}
	SDL_Surface* sfc = SDL_LoadBMP(file.c_str());
	SDL_SetColorKey(sfc, transparencyKey > 0, transparencyKey);
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
/// SGSystem...
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
	else if (e.type == SDL_KEYDOWN && e.key.keysym.sym == SDLK_RETURN && SDL_GetModState() & KMOD_ALT) {
		Window->ToggleFullscreen();
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
	SGUtil::ShowMsgBox("Runtime Error", message, SGMsgBoxType::Error);
	SDL_Quit();
	exit(0);
}
/// SGWindow...
SGWindow::SGWindow() {
};
SGWindow::~SGWindow() {
	Close();
};
void SGWindow::Open(int hRes, int vRes, int layers, int sizeMultiplier) {
	sizeMultiplier++;
	Open(hRes, vRes, layers, sizeMultiplier * hRes, sizeMultiplier * vRes);
}
void SGWindow::Open(int hRes, int vRes, int layers, int hSize, int vSize) {
	if (layers < 0 || layers > 100) {
		SGSystem::Abort(SGString::Format("Invalid layer count: %i", layers));
		return;
	}

	ResWidth = hRes;
	ResHeight = vRes;
	WndWidth = hSize;
	WndHeight = vSize;
	Full = false;
	
	SDL_SetHint(SDL_HINT_RENDER_DRIVER, "direct3d");
	SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "nearest");

	Wnd = SDL_CreateWindow("",
		SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, WndWidth, WndHeight, 0);

	Rend = SDL_CreateRenderer(Wnd, -1,
		SDL_RENDERER_PRESENTVSYNC | SDL_RENDERER_ACCELERATED | SDL_RENDERER_TARGETTEXTURE);

	SDL_RenderSetLogicalSize(Rend, hRes, vRes);

	for (int i = 0; i < layers; i++)
		Layers.push_back(SGLayer());

	Clear();
	Update();
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
void SGWindow::Clear() {
	const int r = (BackColor & 0xff0000) >> 16;
	const int g = (BackColor & 0x00ff00) >> 8;
	const int b = (BackColor & 0x0000ff);
	SDL_SetRenderDrawColor(Rend, r, g, b, SDL_ALPHA_OPAQUE);
	SDL_RenderClear(Rend);
}
void SGWindow::Update() {
	Clear();
	for (auto& layer : Layers) {
		if (layer.Enabled) {
			for (auto& sprite : layer.Sprites) {
				SDL_Rect dst;
				dst.x = sprite.Dst.x + layer.ScrollX;
				dst.y = sprite.Dst.y + layer.ScrollY;
				dst.w = sprite.Dst.w;
				dst.h = sprite.Dst.h;
				SDL_RenderCopy(Rend, sprite.Image->Texture, &sprite.Src, &dst);
			}
		}
	}
	SDL_RenderPresent(Rend);
}
SDL_Renderer* SGWindow::GetRenderer() {
	return Rend;
}
void SGWindow::SetFullscreen(bool full) {
	Uint32 fullscreenFlag = SDL_WINDOW_FULLSCREEN_DESKTOP;
	Uint32 isFullscreen = SDL_GetWindowFlags(Wnd) & fullscreenFlag;

	if ((full && isFullscreen) || (!full && !isFullscreen))
		return;

	SDL_SetWindowFullscreen(Wnd, full ? fullscreenFlag : 0);
	SDL_ShowCursor(isFullscreen);
}
void SGWindow::ToggleFullscreen() {
	Uint32 fullscreenFlag = SDL_WINDOW_FULLSCREEN_DESKTOP;
	Uint32 isFullscreen = SDL_GetWindowFlags(Wnd) & fullscreenFlag;
	SDL_SetWindowFullscreen(Wnd, isFullscreen ? 0 : fullscreenFlag);
	SDL_ShowCursor(isFullscreen);
	Update();
}
void SGWindow::AssertLayerIndex(int index) {
	if (index < 0 || index >= Layers.size())
		SGSystem::Abort(SGString::Format("Layer index out of range: %i", index));
}
void SGWindow::SelectLayer(int index) {
	AssertLayerIndex(index);
	SelectedLayer = index;
}
void SGWindow::EnableLayer(int index, bool enable) {
	AssertLayerIndex(index);
	Layers[index].Enabled = enable;
}
void SGWindow::ScrollLayerToPoint(int index, int x, int y) {
	AssertLayerIndex(index);
	Layers[index].ScrollX = x;
	Layers[index].ScrollY = y;
}
void SGWindow::DrawImage(SGImage* img, int x, int y) {
	Layers[SelectedLayer].AddSprite(img, x, y);
}
void SGWindow::DrawTile(SGImage* img, int tileW, int tileH, int srcX, int srcY, int dstX, int dstY) {
	Layers[SelectedLayer].AddSprite(img, tileW, tileH, srcX, srcY, dstX, dstY);
}
void SGWindow::DeleteAllSprites() {
	for (auto& layer : Layers) {
		layer.Clear();
	}
}
void SGWindow::DeleteSprites(int ixLayer) {
	AssertLayerIndex(ixLayer);
	Layers[ixLayer].Clear();
}
/// SGLayer...
void SGLayer::Clear() {
	Sprites.clear();
}
void SGLayer::AddSprite(SGImage* img, int x, int y) {
	SDL_Rect src;
	src.x = 0;	src.y = 0;	src.w = img->Width;	src.h = img->Height;
	SDL_Rect dst;
	dst.x = x;	dst.y = y;	dst.w = img->Width;	dst.h = img->Height;
	SGSprite sprite;
	sprite.Image = img;
	sprite.Src = src;
	sprite.Dst = dst;
	Sprites.push_back(sprite);
}
void SGLayer::AddSprite(SGImage* img, int tileW, int tileH, int srcX, int srcY, int dstX, int dstY) {
	SDL_Rect src;
	src.x = srcX;	src.y = srcY;	src.w = tileW;	src.h = tileH;
	SDL_Rect dst;
	dst.x = dstX;	dst.y = dstY;	dst.w = tileW;	dst.h = tileH;
	SGSprite sprite;
	sprite.Image = img;
	sprite.Src = src;
	sprite.Dst = dst;
	Sprites.push_back(sprite);
}
/// Main...
SGApiContext* _api = nullptr;

// _BEGIN_DECLS_

int main(int argc, char* argv[]) {
_api = new SGApiContext();
// _BEGIN_MAIN_
delete _api;
return 0;
}

// _BEGIN_DEFS_
