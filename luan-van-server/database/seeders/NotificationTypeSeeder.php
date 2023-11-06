<?php

namespace Database\Seeders;

use App\Models\NotificationType;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class NotificationTypeSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        NotificationType::create([
            'description' => 'Ai đó đã thích bài viết của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã bình luận vào bài viết của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã thích bình luận của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã không thích bình luận của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã không thích bài viết của bạn', 
        ]);

        //

        NotificationType::create([
            'description' => 'Ai đó đã thích bài thảo luận của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã bình luận vào bài thảo luận của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã thích câu trả lời của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã không thích câu trả lời của bạn', 
        ]);

        NotificationType::create([
            'description' => 'Ai đó đã không thích bài thảo luận của bạn', 
        ]);
    }
}
