<?php

use App\Http\Controllers\ClassroomController;
use App\Http\Controllers\HomeController;
use App\Http\Controllers\Moderator\ModeratorClassroom;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\ReportController;
use App\Http\Controllers\StudyDocumentController;

use App\Livewire\Admins\Classrooms\AdminClassroomsIndex;
use App\Livewire\Admins\Classrooms\AdminClassroomsShow;
use App\Livewire\Admins\Games\AdminGameCreate;
use App\Livewire\Admins\Games\AdminGameEdit;
use App\Livewire\Admins\Games\AdminGameIndex;
use App\Livewire\Admins\Moderators\AdminModeratorShow;
use App\Livewire\Admins\PostTemplates\AdminTemplatesCreate;
use App\Livewire\Admins\PostTemplates\AdminTemplatesIndex;
use App\Livewire\Admins\PostTemplates\AdminTemplatesShow;
use App\Livewire\Admins\Reports\AdminReportHistory;
use App\Livewire\Admins\Reports\AdminReportIndex;
use App\Livewire\Admins\Reports\AdminReportShow;
use App\Livewire\Admins\ReportTypes\AdminReportTypeCreate;
use App\Livewire\Admins\ReportTypes\AdminReportTypeIndex;
use App\Livewire\Admins\ReportTypes\AdminReportTypeShow;
use App\Livewire\Admins\SystemNotice\AdminSystemNoticeCreate;
use App\Livewire\Admins\SystemNotice\AdminSystemNoticeIndex;
use App\Livewire\Admins\SystemNotice\AdminSystemNoticeShow;
use App\Livewire\Admins\Users\AdminUsersIndex;
use App\Livewire\Admins\Users\AdminUsersShow;
use App\Livewire\Moderators\Classrooms\Documents\ModeratorDocumentCreateLivewire;
use App\Livewire\Moderators\Classrooms\Documents\ModeratorDocumentEditLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomCreateLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomDetailsLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomEditLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomLivewire;
use App\Livewire\Moderators\Classrooms\QuestionCollections\ModeratorQuestionCollectionCreateLivewire;
use App\Livewire\Moderators\Classrooms\QuestionCollections\ModeratorQuestionCollectionDetailsLivewire;
use App\Livewire\Moderators\Classrooms\QuestionCollections\ModeratorQuestionCollectionEditLivewire;
use App\Livewire\Moderators\Classrooms\Questions\ModeratorQuestionCreateLivewire;
use App\Livewire\Moderators\Classrooms\Questions\ModeratorQuestionEditLivewire;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

Route::get('404', function () {
    return view('errors.404');
})->name("404");


Route::get('error', function () {
    return view('errors.error');
})->name("error");

Route::middleware(['auth', 'role:3'])->group(function () {
    Route::prefix('classrooms')->group(function () {

        Route::get('/', AdminClassroomsIndex::class)->name('classrooms.index.old');
        Route::get('/{id}', [ClassroomController::class, 'show'])->name('classrooms.show.old');
        Route::post('/edit', [ClassroomController::class, 'update'])->name('classrooms.update');

        Route::get('/documents/{id}', [StudyDocumentController::class, 'index'])->name('classrooms.documents.show');
        Route::post('/documents', [StudyDocumentController::class, 'update'])->name('classrooms.documents.update');
    });

    Route::prefix('reports')->group(function () {
        Route::get('/posts', [ReportController::class, 'index'])->name('reports.posts.index');
    });

});

Route::middleware(['auth', 'role:3'])->group(function () {
    Route::prefix('admins')->group(function () {


        Route::prefix('moderators')->group(function () {
            Route::get('/', AdminModeratorShow::class)->name('admin.moderators.show');
        });

        Route::prefix('classrooms')->group(function () {

            Route::get('/', AdminClassroomsIndex::class)->name('classrooms.index');
            Route::get('/{id}', AdminClassroomsShow::class)->name('classrooms.show');

        });

        Route::prefix('reports')->group(function () {
            Route::get('/', AdminReportIndex::class)->name('admin.report.index');
            Route::get('show/{report_id}', AdminReportShow::class)->name('admin.report.show');
            Route::get('history', AdminReportHistory::class)->name('admin.report.history');
        });

        Route::prefix('games')->group(function () {
            Route::get('/', AdminGameIndex::class)->name('admin.game.index');
            Route::get('create', AdminGameCreate::class)->name('admin.game.create');
            Route::get('{game_id}/edit', AdminGameEdit::class)->name('admin.game.edit');
        });

        Route::prefix('users')->group(function () {
            Route::get('/', AdminUsersIndex::class)->name('admin.user.index');
            Route::get('{user_id}/show', AdminUsersShow::class)->name('admin.user.show');

        });

        Route::prefix('post_templates')->group(function () {
            Route::get('/', AdminTemplatesIndex::class)->name('admin.template.index');
            Route::get('{template_id}/show', AdminTemplatesShow::class)->name('admin.template.show');
            Route::get('create', AdminTemplatesCreate::class)->name('admin.template.create');

        });

        Route::prefix('report_types')->group(function () {
            Route::get('/', AdminReportTypeIndex::class)->name('admin.report-type.index');
            Route::get('{report_type_id}/show', AdminReportTypeShow::class)->name('admin.report-type.show');
            Route::get('{model_type}/create', AdminReportTypeCreate::class)->name('admin.report-type.create');

        });

        Route::prefix('system_notifications')->group(function () {
            Route::get('/', AdminSystemNoticeIndex::class)->name('admin.system-notification.index');
            Route::get('create', AdminSystemNoticeCreate::class)->name('admin.system-notification.create');
            Route::get('{notification_id}/show', AdminSystemNoticeShow::class)->name('admin.system-notification.show');

        });
    });
});

Route::middleware(['auth', 'role:2'])->group(function () {
    Route::prefix('moderators')->group(function () {
        Route::prefix('classrooms')->group(function () {
            Route::get('/', ModeratorClassroomLivewire::class)->name('moderator.classrooms.index');
            Route::get('create', ModeratorClassroomCreateLivewire::class)->name('moderator.classrooms.create');
            Route::get('show/{id}', ModeratorClassroomDetailsLivewire::class)->name('moderator.classrooms.show');
            Route::get('edit/{id}', ModeratorClassroomEditLivewire::class)->name('moderator.classrooms.edit');

            Route::prefix('{classroom_id}/question-collections')->group(function () {
                Route::get('create', ModeratorQuestionCollectionCreateLivewire::class)->name('moderator.question-collections.create');
                Route::get('show/{question_collection_id}', ModeratorQuestionCollectionDetailsLivewire::class)->name('moderator.question-collections.show');
                Route::get('edit/{question_collection_id}', ModeratorQuestionCollectionEditLivewire::class)->name('moderator.question-collections.edit');

                Route::prefix('{question_collection_id}/questions')->group(function () {
                    Route::get('create', ModeratorQuestionCreateLivewire::class)->name('moderator.questions.create');
                    Route::get('edit/{question_id}', ModeratorQuestionEditLivewire::class)->name('moderator.questions.edit');

                });
            });

            Route::prefix('{classroom_id}/documents')->group(function () {
                Route::get('create', ModeratorDocumentCreateLivewire::class)->name('moderator.documents.create');
                Route::get('edit/{study_document_id}', ModeratorDocumentEditLivewire::class)->name('moderator.documents.edit');

            });
        });
    });
});



Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware(['auth', 'verified', "role:2"])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
});

require __DIR__ . '/auth.php';