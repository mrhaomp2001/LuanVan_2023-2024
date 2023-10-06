<?php

namespace Database\Seeders;

use App\Models\Notification;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class NotificationSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //

        Notification::create([
            'user_id' => '1',
            'sender_id' => '2',
            'model_id' => '1',
            'notification_type_id' => '1'
        ]);

        Notification::create([
            'user_id' => '1',
            'sender_id' => '2',
            'model_id' => '1',
            'notification_type_id' => '2'
        ]);

        Notification::create([
            'user_id' => '1',
            'sender_id' => '2',
            'model_id' => '1',
            'notification_type_id' => '3'
        ]);
    }
}
