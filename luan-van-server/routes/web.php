<?php

use App\Http\Controllers\ClassroomController;
use App\Http\Controllers\HomeController;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\ReportController;
use App\Http\Controllers\StudyDocumentController;
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
Route::middleware('auth')->group(function () {
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

Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware(['auth', 'verified'])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
});

require __DIR__ . '/auth.php';