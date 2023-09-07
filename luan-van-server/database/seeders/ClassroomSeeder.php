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
        $faker = Factory::create();
        for ($i = 1; $i <= 10; $i++) {
            $classroom = new Classroom;
            $classroom->name = 'Lớp học ' . $i;
            $classroom->description = "Miêu tả lớp học " . $i;
            $classroom->theme_color = $faker->hexColor();
            $classroom->created_at = Carbon::now()->subMinutes($i);
            $classroom->save();
        }
    }
}