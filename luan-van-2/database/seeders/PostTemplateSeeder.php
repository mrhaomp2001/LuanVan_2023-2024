<?php

namespace Database\Seeders;

use App\Models\PostTemplate;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Faker\Factory;

class PostTemplateSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        $faker = Factory::create();
        for ($i = 1; $i < 4; $i++) {
            $hex = $faker->hexColor();
            PostTemplate::create(
                [
                    'name' => "Mẫu số " . $i,
                    'content' => "Nội dung quy định mẫu số " . $i,
                    'theme_color' => $hex,
                ]
            );
        }

    }
}