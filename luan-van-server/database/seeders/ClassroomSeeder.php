<?php

namespace Database\Seeders;

use App\Models\Classroom;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Faker\Factory;
use Carbon\Carbon;

class ClassroomSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //

        $classroom = new Classroom;
        $classroom->name = 'Sinh học 6';
        $classroom->description = "Lớp học được dựa trên sách giáo khoa Sinh học 6, mà chủ yếu học về thực vật";
        $classroom->theme_color = "#006400";
        $classroom->user_id = 1;
        $classroom->is_open = true;
        $classroom->save();

        $faker = Factory::create();
        for ($i = 1; $i <= 10; $i++) {
            $classroom = new Classroom;
            $classroom->name = 'Lớp học ' . $i;
            $classroom->description = "Miêu tả lớp học " . $i;
            $classroom->theme_color = $faker->hexColor();
            $classroom->user_id = 1;
            $classroom->created_at = Carbon::now()->subMinutes($i);
            $classroom->save();
        }
    }
}