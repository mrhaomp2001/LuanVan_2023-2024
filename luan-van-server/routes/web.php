<?php

use App\Http\Controllers\ClassroomController;
use App\Http\Controllers\HomeController;
use App\Http\Controllers\Moderator\ModeratorClassroom;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\ReportController;
use App\Http\Controllers\StudyDocumentController;

use App\Livewire\Moderators\Classrooms\ModeratorClassroomCreateLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomDetailsLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomEditLivewire;
use App\Livewire\Moderators\Classrooms\ModeratorClassroomLivewire;
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

        Route::get('/', [ClassroomController::class, 'index'])->name('classrooms.index');
        Route::get('/{id}', [ClassroomController::class, 'show'])->name('classrooms.show');
        Route::post('/edit', [ClassroomController::class, 'update'])->name('classrooms.update');

        Route::get('/documents/{id}', [StudyDocumentController::class, 'index'])->name('classrooms.documents.show');
        Route::post('/documents', [StudyDocumentController::class, 'update'])->name('classrooms.documents.update');
    });

    Route::prefix('reports')->group(function () {
        Route::get('/posts', [ReportController::class, 'index'])->name('reports.posts.index');
    });

});

Route::middleware(['auth', 'role:2'])->group(function () {
    Route::prefix('moderators')->group(function () {
        Route::prefix('classrooms')->group(function () {
            // Route::get('/', [ModeratorClassroom::class, 'index'])->name('moderator.classrooms.index');
            // Route::get('create', [ModeratorClassroom::class, 'create'])->name('moderator.classrooms.create');
            // Route::post('store', [ModeratorClassroom::class, 'store'])->name('moderator.classrooms.store');

            Route::get('/', ModeratorClassroomLivewire::class)->name('moderator.classrooms.index');
            Route::get('create', ModeratorClassroomCreateLivewire::class)->name('moderator.classrooms.create');
            Route::get('show/{id}', ModeratorClassroomDetailsLivewire::class)->name('moderator.classrooms.show');
            Route::get('edit/{id}', ModeratorClassroomEditLivewire::class)->name('moderator.classrooms.edit');

        });
    });
});



Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware(['auth', 'verified'])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
});

require __DIR__ . '/auth.php';