<?php

namespace Database\Seeders;

use App\Models\Classroom;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class ClassroomSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        for ($i = 1; $i <= 10; $i++) {
            $classroom = new Classroom;
            $classroom->name = 'Lớp học ' . $i;
            $classroom->description = "Miêu tả lớp học " . $i;
            $classroom->theme_color = "#3C5A8C";
            $classroom->save();
        }
    }
}