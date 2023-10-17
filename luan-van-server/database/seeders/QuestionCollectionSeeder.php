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
        $questionCollection = new QuestionCollection();

        $questionCollection->classroom_id = 1;
        $questionCollection->name = "Sinh học nhập môn";
        $questionCollection->difficulty = "Tương đối dễ";
        $questionCollection->game_id = 1;
        $questionCollection->questions_per_time = 5;

        $questionCollection->save();

        $questionCollection = new QuestionCollection();

        $questionCollection->classroom_id = 1;
        $questionCollection->name = "Sinh học cho kỳ thi";
        $questionCollection->difficulty = "trung bình";
        $questionCollection->game_id = 2;
        $questionCollection->questions_per_time = 6;

        $questionCollection->save();
    }
}