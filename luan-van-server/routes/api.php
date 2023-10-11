<?php

use App\Http\Controllers\AnsweredQuestionController;
use App\Http\Controllers\ClassroomController;
use App\Http\Controllers\ClassroomTopicController;
use App\Http\Controllers\ClassroomTopicLikeController;
use App\Http\Controllers\CommentController;
use App\Http\Controllers\CommentLikeController;
use App\Http\Controllers\FriendController;
use App\Http\Controllers\GameApiController;
use App\Http\Controllers\NotificationController;
use App\Http\Controllers\PostController;
use App\Http\Controllers\PostLikeController;
use App\Http\Controllers\PostTemplateController;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\ReportController;
use App\Http\Controllers\ReportTypeController;
use App\Http\Controllers\StudyDocumentController;
use App\Http\Controllers\TopicCommentController;
use App\Http\Controllers\TopicCommentLikeController;
use App\Models\ReportType;
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
Route::post('/test', [GameApiController::class, 'testApi']);


Route::get('/questions', [GameApiController::class, 'getQuestions']);

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

Route::post('user/avatar/edit', [ProfileController::class, 'updateAvatar']);

Route::get('/user/posts', [PostController::class, 'getUserPosts']);
Route::get('/user/posts/old', [PostController::class, 'getUserOldPosts']);

Route::get('/user/friends', [FriendController::class, 'getAcceptedFriends']);
Route::get('/user/friends/waiting', [FriendController::class, 'getWaitingFriends']);

Route::post('/user/friend/edit', [FriendController::class, 'updateFriendStatus']);

Route::get('/classrooms', [ClassroomController::class, 'getClassrooms']);
Route::get('/classrooms/user', [ClassroomController::class, 'getUserClassrooms']);
Route::get('/classrooms/old', [ClassroomController::class, 'getOldClassrooms']);
Route::post('/classrooms/edit', [ClassroomController::class, "updateApi"]);

Route::get('/classroom/info', [ClassroomController::class, 'getClassroomInfo']);
Route::post('/classroom/user/edit', [ClassroomController::class, 'updateStudyStatus']);

Route::prefix('classrooms')->group(function () {
    Route::prefix('topics')->group(function () {
        Route::get('/', [ClassroomTopicController::class, 'getTopics']);
        Route::get('old', [ClassroomTopicController::class, 'getOldTopics']);
        Route::post('/', [ClassroomTopicController::class, 'uploadATopic']);
        Route::post('edit', [ClassroomTopicController::class, 'updateATopic']);
        Route::post('like', [ClassroomTopicLikeController::class, 'updateTopicLike']);

        Route::prefix('comments')->group(function () {
            Route::get('/', [TopicCommentController::class, 'getTopicComments']);
            Route::get('old', [TopicCommentController::class, 'getOldTopicComments']);
            Route::post('/', [TopicCommentController::class, 'uploadATopicComment']);

            Route::post('edit', [TopicCommentController::class, 'updateATopicComment']);
            Route::post('like', [TopicCommentLikeController::class, 'updateLikeStatus']);
        });
    });
});




Route::get('/classroom/documents', [StudyDocumentController::class, 'getStudyDocuments']);

Route::prefix('reports')->group(function () {
    Route::get('/', [ReportController::class, 'getReports']);
    Route::post('/', [ReportController::class, 'createReport']);
    Route::get('posts/types', [ReportTypeController::class, 'getReportPostsTypes']);
    Route::get('comments/types', [ReportTypeController::class, 'getReportCommentsTypes']);
    Route::get('topics/types', [ReportTypeController::class, 'getReportTopicsTypes']);
    Route::get('topic_comments/types', [ReportTypeController::class, 'getReportTopicCommentsTypes']);
});
Route::post('/answered_question', [AnsweredQuestionController::class, 'answeredQuestion']);

Route::prefix('rank')->group(function () {
    Route::get('/day', [AnsweredQuestionController::class, 'getRanksDay']);
    Route::get('/week', [AnsweredQuestionController::class, 'getRanksWeek']);
    Route::get('/month', [AnsweredQuestionController::class, 'getRanksMonth']);
    Route::get('/question_collections/day', [AnsweredQuestionController::class, 'getRanksDayQuestionCollection']);
    Route::get('/classrooms/day', [AnsweredQuestionController::class, 'getRanksDayClassroom']);
    Route::get('/classrooms/week', [AnsweredQuestionController::class, 'getRanksWeekClassroom']);
    Route::get('/classrooms/month', [AnsweredQuestionController::class, 'getRanksMonthClassroom']);

});

Route::prefix('notifications')->group(function () {
    Route::get('/users', [NotificationController::class, 'getNotifications']);
});