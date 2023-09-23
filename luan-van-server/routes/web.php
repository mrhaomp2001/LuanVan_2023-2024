<?php

use App\Http\Controllers\ClassroomController;
use App\Http\Controllers\HomeController;
use App\Http\Controllers\ProfileController;
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
    Route::get('/classrooms', [ClassroomController::class, 'index'])->name('classrooms.index');
    Route::get('/classrooms/{id}', [ClassroomController::class, 'show'])->name('classrooms.show');
    Route::get('/classrooms/documents/{id}', [StudyDocumentController::class, 'index'])->name('classrooms.documents.show');
    Route::post('/classrooms/documents', [StudyDocumentController::class, 'update'])->name('classrooms.documents.update');
    Route::post('/classrooms', [ClassroomController::class, 'update'])->name('classrooms.update');
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