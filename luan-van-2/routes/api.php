<?php

use App\Http\Controllers\GameApiController;
use App\Http\Controllers\PostTemplateController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AnswerController;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::get("/user", function () {
    return "\"message\": \"hello world\"";
});

Route::Resources(["/answers" => AnswerController::class]);

Route::get('/questions', [GameApiController::class, 'getQuestions']);

Route::get('/classrooms', [GameApiController::class, 'getClassrooms']);

Route::get('/login', [GameApiController::class, 'login']);
Route::post('/register', [GameApiController::class, 'register']);

Route::get('/posts', [GameApiController::class, 'getPosts']);
Route::post('/posts', [GameApiController::class, 'uploadAPost']);

Route::get('/post-templates', [PostTemplateController::class, 'indexApi']);
Route::post('/post-templates', [PostTemplateController::class, 'storeApi']);