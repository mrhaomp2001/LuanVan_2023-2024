<?php

namespace Database\Seeders;

use App\Models\QuestionCollection;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class QuestionCollectionSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        for ($i = 1; $i <= 10; $i++) {
            $questionCollection = new QuestionCollection();

            if ($i <= 5) {
                $questionCollection->classroom_id = 1;
            } else {

                $questionCollection->classroom_id = 2;
            }

            $questionCollection->name = "Bộ đề thứ " . $i . ".";
            $questionCollection->difficulty = "Độ khó thử nghiệm";
            $questionCollection->game_type = (string)rand(1, 2);
            $questionCollection->questions_per_time = rand(5, 9);
            
            $questionCollection->save();
        }
    }
}