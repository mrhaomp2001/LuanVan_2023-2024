<?php

use App\Http\Controllers\ClassroomController;
use App\Http\Controllers\ClassroomTopicController;
use App\Http\Controllers\CommentController;
use App\Http\Controllers\CommentLikeController;
use App\Http\Controllers\FriendController;
use App\Http\Controllers\GameApiController;
use App\Http\Controllers\PostController;
use App\Http\Controllers\PostLikeController;
use App\Http\Controllers\PostTemplateController;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\TopicCommentController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

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

Route::get('/test', [GameApiController::class, 'testApi']);


Route::get('/questions', [GameApiController::class, 'getQuestions']);

Route::get('/classrooms', [GameApiController::class, 'getClassrooms']);
Route::get('/classrooms/old', [GameApiController::class, 'getOldClassrooms']);

Route::post('/classrooms/edit', [ClassroomController::class, "updateApi"]);

Route::get('/login', [GameApiController::class, 'login']);
Route::post('/register', [GameApiController::class, 'register']);

Route::get('/posts', [PostController::class, 'getPosts']);
Route::get('/posts/old', [PostController::class, 'getOldPosts']);
Route::post('/posts', [PostController::class, 'uploadAPost']);
Route::post('/post/edit', [PostController::class, 'updatePost']);


Route::post('/post/like', [PostLikeController::class, 'createOrUpdateLikeStatus']);

Route::get('/post-templates', [PostTemplateController::class, 'indexApi']);
Route::post('/post-templates', [PostTemplateController::class, 'storeApi']);

Route::get('post/comments', [CommentController::class, 'getComments']);
Route::get('post/comments/old', [CommentController::class, 'getOldComments']);
Route::post('post/comments', [CommentController::class, 'storeApi']);
Route::post('post/comment/edit', [CommentController::class, 'updateApi']);

Route::post('post/comment/like', [CommentLikeController::class, 'createOrUpdateLikeStatus']);

Route::get('users/login', [GameApiController::class, 'getUserByLatestLogin']);
Route::get('user/info', [ProfileController::class, 'getUserInfomationsApi']);
Route::get('/user/posts', [PostController::class, 'getUserPosts']);
Route::get('/user/posts/old', [PostController::class, 'getUserOldPosts']);

Route::get('/user/friends', [FriendController::class, 'getAcceptedFriends']);
Route::get('/user/friends/waiting', [FriendController::class, 'getWaitingFriends']);

Route::post('/user/friend/edit', [FriendController::class, 'updateFriendStatus']);

Route::get('/classroom/topics', [ClassroomTopicController::class, 'getTopics']);
Route::Post('/classroom/topic', [ClassroomTopicController::class, 'uploadATopic']);
Route::Post('/classroom/topic/edit', [ClassroomTopicController::class, 'updateATopic']);

Route::get('/classroom/topic/comments', [TopicCommentController::class, 'getTopicComments']);
Route::post('/classroom/topic/comments', [TopicCommentController::class, 'uploadATopicComment']);
Route::post('/classroom/topic/comment/edit', [TopicCommentController::class, 'updateATopicComment']);

